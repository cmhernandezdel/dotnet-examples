# Custom functions in lambdas
In this project, I show an example of how to implement custom functions in lambda expressions for Entity Framework Core.

## The problem
Sometimes, particularly when we are using DDD, it would be handy if we could just use a custom function in a lambda, some function that cannot be translated directly to SQL but that we know the translation. If we are using a micro ORM we can just write the SQL query, but when using Entity Framework, things can get messy very quickly.

In this example, we have a custom Value Object representing a [semantic version](https://semver.org/), and we have a function called `IsPrerelease` that will return `true` if the version contains a hyphen (e.g. 1.0.0-beta) and `false` otherwise (e.g. 1.0.0). This could be represented by the following code:

```csharp
public bool IsPrerelease()
{
    return Value.Contains('-');
}
```

The problem comes when we try to use this in a query:

```csharp
context.Applications.Where(a => a.Version.IsPrerelease());
```

We will get an exception because the database provider doesn't know how to translate this query into SQL.

## The solution
The solution is achieved by implementing our custom SQL translation for the function. However, there are downsides to this approach that you must know:
* The method must be an extension method.
* The body of the method is irrelevant because it will be translated using our translation function. This may lead to problems in the future because someone might change the code thinking that they are solving a bug, only to find that the code that they write is irrelevant. 
* If you need multiple translations, the code can get very tangled fast.

## How to launch
You will need docker installed on your system. Running the following command on the root directory should be enough:

```
docker-compose up
```

After this, you can find the Swagger index available on `https://localhost:5000/swagger/index.html`.

## More information
You can find the complete article in [my blog](https://cmhernandezdel.hashnode.dev/utilizando-funciones-propias-en-lambdas-de-entity-framework-core) (in Spanish).