## Vreval-Platform Adapter

This little project helps making api request against the VREVAL rest api. Currently it only supports getting/posting markers and retrieving component defaults. In time this project is supposed to be able to make common requests needed, for third party C# applications to function and to encapsulate some of the complexity that may be necessary for such requests.

# Usage

## .env

```text
BASE_URL=http://vreval.test
API_BASE_URL=http://vreval.test/api/v1
APP_TOKEN=

// This variable should store the Access-Token you will find on the Vreval platform in the "How to create checkpoints" section
CHECKPOINT_CREDS=
```

This project contains an .env.example file. Fill in the appropriate information for your environment. Then load this data during runtime. Read [this article by "Dusted Codes"](https://dusted.codes/dotenv-in-dotnet) explaining how.

## Getting markers
```c#
public static void Main(string[] args)
{
    var dotEnv = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    DotEnv.Load(dotEnv);

    var platform = new PlatformAdapter(Environment.GetEnvironmentVariable("API_BASE_URL"));
    var creds = Environment.GetEnvironmentVariable("CHECKPOINT_CREDS").Split('.');
    
    var res = platform.GetCheckpoints(creds[0], creds[1]);
    var checkpoints = platform.DeserializeCheckpoints(res.Content);
    checkpoints.ForEach(checkpoint => Console.WriteLine(checkpoint.Name));
}
```

## Posting markers
```c#
public static void Main(string[] args)
{
    var dotEnv = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    DotEnv.Load(dotEnv);

    var platform = new PlatformAdapter(Environment.GetEnvironmentVariable("API_BASE_URL"));
    var creds = Environment.GetEnvironmentVariable("CHECKPOINT_CREDS").Split('.');
    
    var checkpoints = new List<Checkpoint>()
    {
        new Checkpoint()
        {
            Name = "Test",
            TypeId = 7,
            CadId = 123,
            Template = new CheckpointTemplate()
        },
        new Checkpoint()
        {
            Name = "Test",
            TypeId = 8,
            CadId = 123,
            Template = new CheckpointTemplate()
        },
        new Checkpoint()
        {
            Name = "Test",
            TypeId = 9,
            CadId = 123,
            Template = new CheckpointTemplate()
        },
    };
    var response = platform.PostCheckpoints(platform.SerializeCheckpoints(checkpoints), creds[0], creds[1]);
    Console.WriteLine(response.Content);
}
```

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
