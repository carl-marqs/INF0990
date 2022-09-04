namespace JewelCollector;

/// <summary>
/// 
/// </summary>
internal struct Position
{
    #region Fields
    internal readonly byte x;
    internal readonly byte y;
    #endregion Fields

    #region Constructor
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    internal Position(byte y, byte x)
    {
        this.y = y;
        this.x = x;
    }
    #endregion Constructor

    #region Visible Methods
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"({x}, {y})";
    }
    #endregion Visible Methods
}