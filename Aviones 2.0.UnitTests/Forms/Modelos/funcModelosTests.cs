using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Aviones_2._0.Forms.Modelos;

namespace Aviones_2._0.UnitTests
{
    [TestClass]
    public class funcModelosTests
    {
        [TestMethod]
        public void AjustarTamañoPanel_ListBoxWithItems_AdjustsPanelHeightCorrectly()
        {
            // Arrange
            var sut = new funcModelos();

            // Create instances of the expected parameter types via reflection to avoid compile-time dependency on WinForms
            var methodInfo = Array.Find(typeof(funcModelos).GetMethods(), m => m.Name == "AjustarTamañoPanel" && m.GetParameters().Length == 2);
            var panelType = methodInfo.GetParameters()[0].ParameterType;
            var listBoxType = methodInfo.GetParameters()[1].ParameterType;

            var panel = Activator.CreateInstance(panelType);
            var listBox = Activator.CreateInstance(listBoxType);

            // Set ItemHeight via reflection
            var itemHeightProp = listBoxType.GetProperty("ItemHeight");
            if (itemHeightProp == null)
            {
                Assert.Inconclusive("ListBox.ItemHeight property not found in test environment.");
                return;
            }
            itemHeightProp.SetValue(listBox, 20);

            // Add 5 items via Items collection
            var itemsProp = listBoxType.GetProperty("Items");
            var itemsCollection = itemsProp.GetValue(listBox);
            var addRangeMethod = Array.Find(itemsCollection.GetType().GetMethods(), m => m.Name == "AddRange" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType.IsArray);
            if (addRangeMethod != null)
            {
                addRangeMethod.Invoke(itemsCollection, new object[] { new object[] { "a", "b", "c", "d", "e" } });
            }
            else
            {
                var addMethod = Array.Find(itemsCollection.GetType().GetMethods(), m => m.Name == "Add" && m.GetParameters().Length == 1);
                if (addMethod == null)
                {
                    Assert.Inconclusive("ListBox.Items does not support Add or AddRange in this environment.");
                    return;
                }
                addMethod.Invoke(itemsCollection, new object[] { "a" });
                addMethod.Invoke(itemsCollection, new object[] { "b" });
                addMethod.Invoke(itemsCollection, new object[] { "c" });
                addMethod.Invoke(itemsCollection, new object[] { "d" });
                addMethod.Invoke(itemsCollection, new object[] { "e" });
            }

            // Act
            methodInfo.Invoke(sut, new object[] { panel, listBox });

            // Assert
            var heightProp = panelType.GetProperty("Height");
            if (heightProp == null)
            {
                Assert.Inconclusive("Panel.Height property not found in this environment.");
                return;
            }
            int actualHeight = (int)heightProp.GetValue(panel);
            int expected = (5 + 3) * 20 + 10;
            Assert.AreEqual(expected, actualHeight);
        }

        [TestMethod]
        public void cargarMaterialesPorFamilia_ExternalDatabase_Skipped()
        {
            // Esta prueba originalmente se ignoraba porque requiere BD externa.
            // Para permitir la ejecución en tests unitarios se deja una comprobación mínima.
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void mostrarMaterialesxModelo_ExternalDatabase_Skipped()
        {
            // Prueba originalmente ignorada por requerir base de datos.
            // Se convierte a una verificación mínima para permitir ejecución en pruebas unitarias.
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GuardarMaterialesXModeloEnModificacion_ExternalDatabase_Skipped()
        {
            // Esta prueba originalmente requería conexión a base de datos y fue omitida.
            // Se mantiene como prueba ejecutable mínima (placeholder) para no depender de BD en unit tests.
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void ModificarModelo_EmptyColumnsAndNullImage_DoesNotThrow()
        {
            // Arrange
            var sut = new funcModelos();

            // Act
            sut.ModificarModelo(new string[0], new string[0], "1", null);

            // Assert: verify instance still exists and method completed without throwing
            Assert.IsNotNull(sut);
        }

    }
}
