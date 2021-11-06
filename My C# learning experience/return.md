# Return in C#
### if there have any error, please be considerate and report in `Issues`
### `Long Code`

First see this C# code
```cs
private static bool my_method(int i)()
{
    if (i == 0)
    {
        return true;
    }
    else
    {
        return false;
    }
}
```
 the code is too long, and we can still keep it shorter
### `Shorter code`
```cs
private static bool my_method(int i)
{
    return i == 0;
}
```
Code explanation:
if `i == 0` return true, else return false.

### `Simplest Code`
You can use lambda experssion to simpler the code:
```cs
private static bool my_lambda(int i) => i == 0;
```
------
© Copyright by Goldhahaha
