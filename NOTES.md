# Aviones – behavior notes

Spec for the refactor: what each part should do, what it does today, and open questions for the author.

Status comes from reading the code (2026-10-14), not from running every screen. Line references point at the code as received (commit `b38d95c`).

## Purpose

Stock control for building model airplanes:

- **Materiales**: everything in stock (wood, covering, electronics, hardware…), with quantity, value, location and photo.
- **Modelos**: the airplanes. Each one records which materials (and how much) went into it. Assigning material to a model discounts it from stock.
- **Compras**: purchasing. Out of scope for now.

## Materiales

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

Expected: list of airplanes; create one with its material composition (discounting stock); edit; delete.

| Feature | Status | Notes |
|---|---|---|
| List models | ✅ Works | Name, alias, state, date; photo on hover. |
| Create model | ❌ Broken (core flow) | • **Picked materials are saved with the wrong ID**: the code reads column 15 (`IdAcopio`, empty) instead of 14 (`IdMaterial`) (`frmMaterialporModelo.cs:562-563`). The model is saved, but its materials point to nothing and **stock is not discounted**.<br>• Decimal quantities: "1.5" is turned into "1,5" and SQL rejects it (`frmMaterialporModelo.cs:523, 566`).<br>• Date is sent as `dd/MM/yyyy` text; SQL Server reads it as month/day, so days above 12 fail and the rest swap day and month (`frmIngreso.cs:195`).<br>• Removing a picked material before saving doesn't remove it from what gets saved (`frmMaterialporModelo.cs:437`).<br>• Pressing Enter in Alias always opens the photo picker. |
| Edit model | ❌ Broken | • Changes to name/alias/state/date are never saved (the list of changed fields is never filled).<br>• Adding materials: same wrong-ID bug as create.<br>• Removing a material crashes (reads the empty column 15) and uses the wrong columns for ID and quantity (`frmMaterialporModelo.cs:469-471`).<br>• Changing the photo works. |
| Delete model | ❌ Missing | ELIMINAR button isn't wired to any code. |
| LISTADOS | ❌ Missing | Button isn't wired to any code. |

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
4. Al quitar un material de un modelo, el programa pregunta "¿Este material se recicla?": si se recicla vuelve al stock, si no se pierde. ¿Es correcto?
5. Filtros: ¿por qué campos necesita buscar? (familia, grupo, fabricante, ubicación, sin stock, …)
6. "Ubicación": ¿es dónde se guarda el material (caja, estante) o en qué avión está?
7. Al eliminar un modelo, ¿los materiales vuelven al stock?
8. El estado del material (Nuevo/Usado), ¿se usa para algo más que mostrarlo?
9. El selector de materiales excluye una familia "Aeronaves". ¿Había una familia para aviones completos?
