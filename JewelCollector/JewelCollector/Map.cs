using Microsoft.Extensions.Logging;

namespace JewelCollector;

/// <summary>
/// Representa o mapa 2D do jogo.
/// </summary>
internal class Map
{
    #region Fields
    private readonly ILogger<Map> _logger;
    private readonly Placeable?[,] _map;
    #endregion Fields

    #region Constructor
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    internal Map(ILogger<Map> logger, byte width, byte height)
    {
        _logger = logger;
        _map = new Placeable?[width, height];
    }
    #endregion Constructor

    #region Visible Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    internal Jewel? GetAdjacentJewel(Position position)
    {
        foreach (IMoveable.MoveDirections direction in Enum.GetValues(typeof(IMoveable.MoveDirections)).Cast<IMoveable.MoveDirections>())
        {
            Position nextPosition = GetNextPosition(position, direction);
            if (IsPositionValid(nextPosition) && _map[nextPosition.y, nextPosition.x] is Jewel jewel)
            {
                _map[nextPosition.y, nextPosition.x] = null;
                return jewel;
            }
        }

        _logger.LogWarning("There are no jewels adjacent to {Position}", position);
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placeable"></param>
    /// <param name="direction"></param>
    internal void Move(Placeable placeable, IMoveable.MoveDirections direction)
    {
        if (placeable is IMoveable moveable)
        {
            Position nextPosition = GetNextPosition(placeable.Position, direction);
            if (IsPositionValid(nextPosition))
            {
                if (!IsPositionBlocked(nextPosition))
                {
                    _map[placeable.Position.y, placeable.Position.x] = null;
                    _map[nextPosition.y, nextPosition.x] = placeable;
                    moveable.Move(nextPosition);
                }
                else
                    _logger.LogWarning("Blocked position: {Position}", nextPosition);
            }
            else
                _logger.LogWarning("Invalid position: {Position}", nextPosition);
        }
        else
            _logger.LogWarning("{Name} is not moveable.", placeable.GetType());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placeable"></param>
    internal void Place(Placeable placeable)
    {
        if (_map[placeable.Position.y, placeable.Position.x] == null)
        {
            _map[placeable.Position.y, placeable.Position.x] = placeable;
            _logger.LogDebug("Added {Name} to {Position}", placeable.GetType(), placeable.Position);
        }
        else
            _logger.LogWarning("Position {Position} already occupied.", placeable.Position);
    }

    /// <summary>
    /// 
    /// </summary>
    internal void Print()
    {
        for (int y = 0; y < _map.GetLength(0); y++)
        {
            for (int x = 0; x < _map.GetLength(1); x++)
            {
                string? symbol = _map[y, x] != null ? _map[y, x]?.Symbol : "--";
                Console.Write($"{symbol} ");
            }
            Console.WriteLine();
        }
    }
    #endregion Visible Methods

    #region Private Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="oldPosition"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    private static Position GetNextPosition(Position oldPosition, IMoveable.MoveDirections direction)
    {
        return direction switch
        {
            IMoveable.MoveDirections.North => new Position((byte)(oldPosition.y - 1), oldPosition.x),
            IMoveable.MoveDirections.West => new Position(oldPosition.y, (byte)(oldPosition.x - 1)),
            IMoveable.MoveDirections.South => new Position((byte)(oldPosition.y + 1), oldPosition.x),
            IMoveable.MoveDirections.East => new Position(oldPosition.y, (byte)(oldPosition.x + 1)),
            _ => oldPosition
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool IsPositionBlocked(Position position)
    {
        return _map[position.y, position.x] != null && _map[position.y, position.x] is Obstacle;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool IsPositionValid(Position position)
    {
        return position.y >= 0 && position.y < _map.GetLength(0) && position.x >= 0 && position.x < _map.GetLength(1);
    }
    #endregion Private Methods
}
