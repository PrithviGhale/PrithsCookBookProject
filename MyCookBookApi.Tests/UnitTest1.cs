using Xunit;
using MyCookBookApi.Controllers;
using Microsoft.AspNetCore.Mvc;

public class RecipeControllerTests
{
    [Fact]
    public void GetRecipes_ShouldReturnOk()
    {
        var controller = new RecipeController();
        var result = controller.GetRecipes();

        Assert.IsType<OkObjectResult>(result);
    }
}
