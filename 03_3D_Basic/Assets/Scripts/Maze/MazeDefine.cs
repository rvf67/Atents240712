using System;

/// <summary>
/// Cell에 어느쪽으로 길이 나있는지 표시하기 위한 bitFlag용 enum
/// </summary>
[Flags]
public enum PathDirection : byte
{
    None =0,
    North =1,
    East =2,
    South=4,
    West=8,
}

[Flags]
public enum CornerMAsk : byte
{
    None =0,
    NorthWest =1,
    NorthEast =2,
    SouthEast =4,
    SouthWest =8,
}

public enum TestDirection : byte
{
    None =0,
    North =1,
    East =2,
    South=4,
    West=8,
}

/// <summary>
/// Cell의 어떤 코너가 보일지 표시하기 위한 bitFlag용 enum
/// </summary>
[Flags]
public enum CornerMask : byte
{
    None = 0,
    NorthWest = 1,
    NorthEast = 2,
    SouthEast = 4,
    SouthWest = 8,
}

