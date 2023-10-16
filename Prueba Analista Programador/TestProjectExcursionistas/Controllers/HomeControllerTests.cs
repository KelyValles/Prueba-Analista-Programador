using Microsoft.AspNetCore.Mvc;
using Moq;
using ProyectoExcursionistas.Controllers;
namespace TestProjectExcursionistas.Controllers
{

    public class HomeControllerTests
    {

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexWithParameters_ReturnsViewResult()
        {
            
            var controller = new HomeController();

            // Example values for minCalorias and maxPeso
            var result = controller.Index(100, 50); 

            
            Assert.IsType<ViewResult>(result);
        }

        
    }
}
