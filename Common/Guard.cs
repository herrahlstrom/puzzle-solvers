using System.Runtime.CompilerServices;

namespace Common;

public static class Guard
{
    public static void FromNull(object obj, [CallerArgumentExpression("obj")] string? argumentExpression = null)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(argumentExpression);
        }
    }
    
}