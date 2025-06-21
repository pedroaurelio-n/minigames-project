using System;

[Serializable]
public struct GameVersion : IComparable<GameVersion>, IEquatable<GameVersion>
{
    public int Major { get; }
    public int Minor { get; }
    public int Patch { get; }

    public GameVersion (
        int major,
        int minor,
        int patch
    )
    {
        Major = major;
        Minor = minor;
        Patch = patch;
    }

    public override string ToString () => $"{Major}.{Minor}.{Patch}";

    public static GameVersion Parse (string versionString)
    {
        if (string.IsNullOrWhiteSpace(versionString))
            throw new ArgumentNullException($"Version string cannot be null or empty.");
        
        string[] parts = versionString.Split('.');
        if (parts.Length != 3)
            throw new FormatException($"Version string must be in format MAJOR.MINOR.PATCH.");

        return new GameVersion(
            int.Parse(parts[0]),
            int.Parse(parts[1]),
            int.Parse(parts[2])
        );
    }

    public int CompareTo (GameVersion other)
    {
        if (Major != other.Major)
            return Major.CompareTo(other.Major);
        if (Minor != other.Minor)
            return Minor.CompareTo(other.Minor);
        return Patch.CompareTo(other.Patch);
    }

    public bool Equals (GameVersion other)
    {
        return Major == other.Major
               && Minor == other.Minor
               && Patch == other.Patch;
    }

    public override bool Equals (object obj) => obj is GameVersion other && Equals(other);

    public override int GetHashCode ()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + Major.GetHashCode();
            hash = hash * 31 + Minor.GetHashCode();
            hash = hash * 31 + Patch.GetHashCode();
            return hash;
        }
    }
    
    public static bool operator == (GameVersion left, GameVersion right) => left.Equals(right);
    public static bool operator != (GameVersion left, GameVersion right) => !left.Equals(right);
    public static bool operator < (GameVersion left, GameVersion right) => left.CompareTo(right) < 0;
    public static bool operator <= (GameVersion left, GameVersion right) => left.CompareTo(right) <= 0;
    public static bool operator > (GameVersion left, GameVersion right) => left.CompareTo(right) > 0;
    public static bool operator >= (GameVersion left, GameVersion right) => left.CompareTo(right) >= 0;
}