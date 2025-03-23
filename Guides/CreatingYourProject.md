### Creating your project

1. Build a new project called `MathAPI` in VS Code of type `ASP.net Core Web API` (not app). Enter this command:
``
dotnet new webapi --use-controllers -o MathAPI
``
1. You will notice that this API has a `Controllers` folder but no views or models.
1. Since we are serving back JSON and not a view, we will not need a `Views` folder. We will need a `Models` folder which you can go ahead and create.
1. Run your app and check out the `/WeatherForecast` endpoint which serves JSON weather forecast data.