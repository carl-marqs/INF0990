namespace JewelCollector;

/// <summary>
/// 
/// </summary>
internal interface IMoveable
{
    #region Enums
    /// <summary>
    /// Possíveis direções que o objeto pode se mover.
    /// </summary>
    internal enum MoveDirections
    {
        North, /// Norte
        West,  /// Oeste
        South, /// Sul
        East   /// Leste
    }
    #endregion Enums

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newPosition"></param>
    public void Move(Position newPosition);
}
