﻿namespace HelpfulTypesAndExtensions;

/// <summary>
/// A GTuid is a globally unique identifier that is generated by appending the current UTC datetime in ticks to a guid
/// The GUID is converted to a string using the N format, which removes the dashes, a single dash is then appended to the end of the guid to seperate it from the datetme
/// </summary>
public readonly record struct Gtuid
{
    private readonly string? _value = null;
    
    internal Gtuid(Guid value)
    {
        //get the current UTC datetime to the nanosecond and append it to the guid
        var tickCount = DateTime.UtcNow.Ticks;
        //do not use the N format for tick count, it creates a format like 123,456,789,123,456,789
        // N format on the Guid however just removes the dashes
        _value = value.ToString("N") + "-" +  tickCount;
    }

    public static Gtuid Create() => new(Guid.NewGuid());
    
    public static Gtuid Create(Guid value) => new(value);
    
    public static Gtuid Create(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new Gtuid(guid);
        }
        throw new ArgumentException("The value provided is not a valid guid");
    }


    public override string ToString() => _value ?? Create()._value!;
    
    public static implicit operator string(Gtuid gtuid) => gtuid._value!;
    public static implicit operator Gtuid(Guid guid) => new(guid);
    
}