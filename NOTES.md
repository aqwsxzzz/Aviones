# Aviones – behavior notes

Spec for the refactor: what each part should do, what it does today, and open questions for the author.

Status comes from reading the code (2026-10-14), not from running every screen. Line references point at the code as received (commit `b38d95c`).

## Purpose

Stock control for building model airplanes:

- **Materiales**: everything in stock (wood, covering, electronics, hardware…), with quantity, value, location and photo.
- **Modelos**: the airplanes. Each one records which materials (and how much) went into it. Assigning material to a model discounts it from stock.
- **Compras**: purchasing. Out of scope for now.

## Materiales

> **Rebuilt on `refactor/data-layer` (2026-10-14).** One screen: filters (family, text that also searches manufacturer and location, "Sin stock", "A comprar"), the full list (zero-stock rows in gray), and for the selected material its photo, lots, and the models that use it. Dialogs for material (with optional first lot), lot, families/units, a value-per-family report, and multi-page printing. The table below describes the **old** code.


Expected: list of all materials (zero stock included), filters, create / edit / delete, optional photo, and see which airplanes use each material.

| Feature | Status | Notes |
|---|---|---|
| List all materials | ⚠️ Works, with a display bug | Shows every material, including zero stock. Cantidad/Disponible are shown as whole numbers, so 3.6 m of covering shows as 3 (`funcMateriales.cs:47-48, 96-97`). Total column is hidden. |
| Filters | ⚠️ Minimal | Only "BUSCAR → pick a family". No search by text, group, manufacturer, location or low stock. |
| Families (add / rename / delete) | ✅ Works | Delete is blocked while the family still has materials. |
| Create material | ✅ Works for whole numbers | INGRESAR → REGISTROS → family → form. Estado, Valor and Cantidad are required. Clicking a cell in the "existing materials" grid copies it into the form. |
| Unit types (Tipo) add / delete | ❌ Not implemented | Buttons exist, handlers are empty (`frmMateriales.cs:1251`). New types can still be typed during create. |
| Edit material | ❌ Partly broken | • Changing **Tipo** is never saved (`frmModificaciones.cs:212-227`).<br>• **Decimal quantities**: Cantidad is saved with integer parsing, so "3.6" becomes 0 (`funcMateriales.cs:603-608`), and `int.Parse` crashes on it (`frmModificaciones.cs:356`).<br>• Changing Cantidad adds the difference to Disponible with SQL math on a text column, which fails when Disponible has decimals (`funcMateriales.cs:673`).<br>• After saving, the grid only shows that material's family. |
| Delete material | ✅ Works | Asks for confirmation; if the material is used in models it warns, then deletes the material and its model links. |
| Photo | ✅ Works | Load on create/edit, shown on row click, double-click to enlarge. |
| Which airplanes use a material | ❌ Missing | `MostrarUbicacionesMaterial` exists but is never called, and would show model IDs instead of names. |
| INFORMES (value totals) | ⚠️ Inconsistent | The general total uses Cantidad (everything ever entered); the per-family total uses Disponible (what's left). |
| IMPRIMIR | ❓ Unverified | Only enabled after filtering by family. |

## Modelos

> **Rebuilt on `refactor/data-layer` (2026-10-14).** List of airplanes (filter by condition or text) with total material value; for the selected one its photo and materials with subtotals. One editor for new and existing models: name, alias, condition, date, photo and materials. Materials are picked by lot ("AGREGAR MATERIALES…" stays open to add several), quantities can be changed, and removing one asks if it's recycled. Nothing touches stock until GUARDAR, and everything is saved together or not at all. ELIMINAR asks whether the materials go back to stock. The table below describes the **old** code.


Expected: list of airplanes; create one with its material composition (discounting stock); edit; delete.

| Feature | Status | Notes |
|---|---|---|
| List models | ✅ Works | Name, alias, state, date; photo on hover. |
| Create model | ❌ Broken (core flow) | • **Picked materials are saved with the wrong ID**: the code reads column 15 (`IdAcopio`, empty) instead of 14 (`IdMaterial`) (`frmMaterialporModelo.cs:562-563`). The model is saved, but its materials point to nothing and **stock is not discounted**.<br>• Decimal quantities: "1.5" is turned into "1,5" and SQL rejects it (`frmMaterialporModelo.cs:523, 566`).<br>• Date is sent as `dd/MM/yyyy` text; SQL Server reads it as month/day, so days above 12 fail and the rest swap day and month (`frmIngreso.cs:195`).<br>• Removing a picked material before saving doesn't remove it from what gets saved (`frmMaterialporModelo.cs:437`).<br>• Pressing Enter in Alias always opens the photo picker. |
| Edit model | ❌ Broken | • Changes to name/alias/state/date are never saved (the list of changed fields is never filled).<br>• Adding materials: same wrong-ID bug as create.<br>• Removing a material crashes (reads the empty column 15) and uses the wrong columns for ID and quantity (`frmMaterialporModelo.cs:469-471`).<br>• Changing the photo works. |
| Delete model | ❌ Missing | ELIMINAR button isn't wired to any code. |
| LISTADOS | ❌ Missing | Button isn't wired to any code. |

## Original data design (before the "Acopio" merge)

Reconstructed from the code and `DB/migrate_acopio_to_materiales.sql`. The original tables are gone from the database we have, so this is what the code expected.

**Materiales**: *what* the material is (catalog entry)

| Column | Meaning |
|---|---|
| IdMaterial | key |
| IdFamilia | → Familiares (Madera, Electrónica, …) |
| Grupo | kind within the family (Balsa, Servo, Batería…) |
| Caracteristica | description (Tabla, Micro 9 g, LiPo 3S…) |
| Color | color; for wood it holds the imperial size |
| Medidas | dimensions |
| Codigo | manufacturer part number |
| Estado | N = Nuevo, U = Usado |
| Tipo | unit (Unidades, Metros…) |
| ImageData | photo |
| Cantacomprar | quantity to buy (used by Compras) |

**Acopio**: one row per *stock lot* of a material (each time it was acquired)

| Column | Meaning |
|---|---|
| IdAcopio | key |
| IdMaterial | → Materiales |
| Fabricante | manufacturer / brand of this lot |
| Ubicacion | where this lot is stored |
| Cantidad | quantity acquired in this lot |
| Disponible | what's left of this lot |
| Valor | unit price paid for this lot |

**MaterialesModelo** stored the **IdAcopio** (the lot) in its `IdMaterial` column: a model consumed from a specific lot, and removing a "recyclable" material returned the quantity to that lot. This explains why the code reads column 15 (`IdAcopio`).

The migration copied only the **latest lot** of each material into `Materiales` (`ROW_NUMBER() … ORDER BY IdAcopio DESC`), so materials with more than one lot lost the older ones, and the code was only half-adapted.

## New data design (branch `refactor/data-layer`)

Decision (2026-10-14): **keep stock lots**. A material is a catalog entry; each acquisition is a lot with its own manufacturer, location, quantity, available and unit price. Models consume from specific lots.

| Table | Replaces | Notes |
|---|---|---|
| `Familias` | `Familiares` | unique name |
| `TiposUnidad` | free-text `Materiales.Tipo` | unique name |
| `EstadosModelo` | free-text `Modelos.Estado` | Activo, En construcción, En reparación, Inactivo |
| `Materiales` | `Materiales` (catalog part) | FK to family and unit type; `Estado` N/U checked |
| `Lotes` | `Acopio` | `Disponible` is a number, `0 ≤ Disponible ≤ Cantidad`; new `FechaIngreso` |
| `Modelos` | `Modelos` | FK to state |
| `MaterialesModelo` | `MaterialesModelo` | references `IdLote`; deleting a model deletes its rows |
| `vMaterialesStock` | — | one row per material with its lots summed |

Quantities are `DECIMAL(18,3)` everywhere, money `DECIMAL(18,2)`.

Data access lives in `Aviones 2.0/Data/`: one repository per table, all SQL parameterized (Dapper), and every stock movement (assign, change, remove, delete model) runs in a transaction that fails as a whole if a lot doesn't have enough. Integration tests in `Aviones 2.0.UnitTests/Data/` run against an `Aviones_Test` database.

Reset the dev database with `Aviones 2.0/DB/reset.ps1`.

The old screens still use the old tables, so they won't work against this schema until they're moved to the repositories.

## Cross-cutting problems

- **"Acopio" leftovers.** Material data used to be split between `Materiales` and an `Acopio` table. A migration merged them, but the code was only half-updated: it still passes an "IdAcopio" around and checks for `Acopio`/`IdAcopioUltimo` at runtime. This is the root cause of the Modelos bugs.
- **Disponible is stored as text**, while Cantidad is a number.
- **Decimals are handled three different ways**: Spanish format (comma), English format (dot), and integer. Anything measured in metres or grams breaks somewhere.
- **No foreign keys**, so deleting things can leave orphan rows.
- **SQL built by concatenating text** in many places: a name with an apostrophe breaks the query and it's open to SQL injection.
- **Fixed pixel positions for a 1920×1080 screen**: other resolutions will misplace controls.
- Every screen switches modes by toggling dozens of controls' visibility by hand, which is why so many paths leave the screen in an odd state.

## Preguntas para el autor

1. ¿"Disponible" debe ser siempre *Cantidad − lo usado en modelos*, o a veces se ajusta a mano?
2. Al modificar la Cantidad de un material, ¿es una compra nueva (suma al stock) o una corrección de un error?
3. ¿Qué era "Acopio"? ¿Hace falta un historial de ingresos/compras de cada material?
4. ~~Al quitar un material de un modelo, ¿se recicla?~~ **Respondido:** sí, se pregunta; si se recicla vuelve al stock.
5. Filtros: ¿por qué campos necesita buscar? (familia, grupo, fabricante, ubicación, sin stock, …)
6. "Ubicación": ¿es dónde se guarda el material (caja, estante) o en qué avión está?
7. ~~Al eliminar un modelo, ¿los materiales vuelven al stock?~~ **Respondido (2026-10-14):** se muestra la lista de materiales del modelo y se marca cuáles se reciclan (motor, batería…), con la cantidad que vuelve; el resto se da por consumido.
8. El estado del material (Nuevo/Usado), ¿se usa para algo más que mostrarlo?
9. El selector de materiales excluye una familia "Aeronaves". ¿Había una familia para aviones completos?
