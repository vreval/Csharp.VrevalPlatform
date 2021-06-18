## Vreval-Platform Adapter

This little project helps making api request against the VREVAL rest api. Currently it only supports getting/posting markers and retrieving component defaults. In time this project is supposed to be able to make common requests needed, for third party C# applications to function and to encapsulate some of the complexity that may be necessary for such requests.

# Usage

## .env

```text
BASE_URL=http://vreval.test
API_BASE_URL=http://vreval.test/api/v1
APP_TOKEN=

// This variable should store the Access-Token you will find on the Vreval platform in the "How to create markers" section
ACCESS_TOKEN=
```

This project contains an .env.example file. Fill in the appropriate information for your environment. Then load this data during runtime. Read [this article by "Dusted Codes"](https://dusted.codes/dotenv-in-dotnet) explaining how.

## Getting markers
```c#
public static async Task Main(string[] args)
{
    var dotEnv = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    DotEnv.Load(dotEnv);

    var platform = new PlatformAdapter(Environment.GetEnvironmentVariable("API_BASE_URL"));
    var accessToken = new AccessToken(Environment.GetEnvironmentVariable("ACCESS_TOKEN"));
    
    var markers = await platform.GetMarkersAsync(accessToken);
    markers.ForEach(checkpoint => Console.WriteLine(checkpoint.Name));
}
```

## Posting markers
```c#
public static async Task Main(string[] args)
{
    var dotEnv = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    DotEnv.Load(dotEnv);

    var platform = new PlatformAdapter(Environment.GetEnvironmentVariable("API_BASE_URL"));
    var accessToken = new AccessToken(Environment.GetEnvironmentVariable("ACCESS_TOKEN"));
    
    var markers = new List<Marker>
    {
        new Marker
        {
            Name = "My test marker 1",
            CadId = 123,
            Template = new MarkerTemplate
            {
                Visibility = MarkerVisibility.inside_perimeter.ToString(),
                CadData = new MarkerCadData
                {
                    MarkerName = "My new marker",
                    MarkerDescription = "Marker description",
                    Position = new[] {1.234f, 14.567f, -123.123f},
                    Type = MarkerType.Checkpoint.ToString()
                },
                PerimeterRadius = 10f,
                PlacementDistance = 1.2f
            }
        },
        new Marker
        {
            Name = "My test marker 2",
            CadId = 123,
            Template = new MarkerTemplate
            {
                Visibility = MarkerVisibility.inside_perimeter.ToString(),
                CadData = new MarkerCadData
                {
                    MarkerName = "My new marker",
                    MarkerDescription = "Marker description",
                    Position = new[] {1.234f, 14.567f, -123.123f},
                    Type = MarkerType.Checkpoint.ToString()
                },
                PerimeterRadius = 7f,
                PlacementDistance = 2f
            }
        }
    };

    await platform.PostMarkersAsync(markers, accessToken);
}
```
Any omitted fields will will keep their default value. If you need different default values when sending markers, just edit the appropriate data object.

## Getting Component Defaults

```c#
public static void Main(string[] args)
{
    var dotEnv = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    DotEnv.Load(dotEnv);
    
    var platform = new PlatformAdapter(Environment.GetEnvironmentVariable("API_BASE_URL"));

    var response = platform.GetComponentDefaults("checkpoint");
    Console.WriteLine(response.Content);
}
```

`GetComponentDefaults` also accepts the following arguments:

- `form-field`
- `task`
- `playlist`
- `evaluation`
