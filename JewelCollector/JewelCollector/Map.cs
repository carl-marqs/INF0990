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
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="logger"> Objeto responsável por registrar mensagens. </param>
    /// <param name="size"> Tamanho do mapa. </param>
    internal Map(ILogger<Map> logger, byte size)
    {
        _logger = logger;
        _map = new Placeable?[size, size];
    }
    #endregion Constructor

    #region Visible Methods
    /// <summary>
    /// Obtém o número de joias no mapa.
    /// </summary>
    /// <returns> Quantidade de joias no mapa. </returns>
    internal ushort CountJewels()
    {
        ushort count = 0;
        for (int y = 0; y < _map.GetLength(0); y++)
            for (int x = 0; x < _map.GetLength(1); x++)
                if (_map[y, x] != null && _map[y, x] is Jewel)
                    count++;
        return count;
    }

    /// <summary>
    /// Obtém um item adjacente a uma dada posição, se houver.
    /// </summary>
    /// <param name="position"> Posição a se verificar. </param>
    /// <returns> Item adjacente, se houver, null caso contrário. </returns>
    internal Placeable? GetAdjacentItem(Position position)
    {
        foreach (IMoveable.MoveDirections direction in Enum.GetValues(typeof(IMoveable.MoveDirections)).Cast<IMoveable.MoveDirections>())
        {
            Position nextPosition = GetNextPosition(position, direction);
            if (IsPositionValid(nextPosition))
            {
                if (_map[nextPosition.y, nextPosition.x] is Jewel jewel)
                {
                    _map[nextPosition.y, nextPosition.x] = null;
                    return jewel;
                }
                else if (_map[nextPosition.y, nextPosition.x] is Obstacle obstacle && obstacle.Type == Obstacle.Types.Tree)
                {
                    _map[nextPosition.y, nextPosition.x] = null;
                    return obstacle;
                }

            }
        }

        LogHelper.QueueLog(_logger, LogEntry.Types.Warning, $"There are no items adjacent to {position}");
        return null;
    }

    /// <summary>
    /// Obtém uma posição no mapa que não é ocupada por nenhum item.
    /// </summary>
    /// <returns> Posição vazia. </returns>
    internal Position GetEmptyPosition()
    {
        Position emptyPosition = new(0, 0);
        Random random = new();

        try
        {
            do
            {
                emptyPosition = new Position((byte)random.Next(_map.GetLength(0)), (byte)random.Next(_map.GetLength(1)));
            }
            while (IsPositionBlocked(emptyPosition));

            return emptyPosition;
        }
        catch (Exception ex)
        {
            LogHelper.QueueLog(_logger, LogEntry.Types.Error, ex.Message, ex);
        }

        return emptyPosition;
    }

    /// <summary>
    /// Move um item para determinada posição.
    /// </summary>
    /// <param name="placeable"> Item a ser movido. </param>
    /// <param name="direction"> Direção a qual mover. </param>
    internal void Move(Placeable placeable, IMoveable.MoveDirections direction)
    {
        try
        {
            if (placeable is IMoveable moveable)
            {
                Position nextPosition = GetNextPosition(placeable.Position, direction);
                if (IsPositionValid(nextPosition))
                {
                    if (!IsPositionBlocked(nextPosition))
                    {
                        // Dano radioativo
                        if (placeable is Robot player)
                        {
                            // Se passar em cima
                            if (_map[nextPosition.y, nextPosition.x] is Obstacle obstacle && obstacle.Type == Obstacle.Types.Radioactive)
                            {
                                player.Energy -= 30;
                                LogHelper.QueueLog(_logger, LogEntry.Types.Warning, "Alto dano radioativo recebido");
                            }
                            // Se estiver adjacente
                            else if (IsAdjacentToRadioactive(nextPosition))
                            {
                                player.Energy -= 10;
                                LogHelper.QueueLog(_logger, LogEntry.Types.Warning, "Dano radioativo recebido");
                            }
                        }

                        // Mover
                        _map[placeable.Position.y, placeable.Position.x] = null;
                        _map[nextPosition.y, nextPosition.x] = placeable;
                        moveable.Move(nextPosition);
                    }
                    else
                        throw new Exception($"Blocked position: {nextPosition}");
                }
                else
                    throw new Exception($"Invalid position: {nextPosition}");
            }
            else
                throw new Exception($"{placeable.GetType()} is not moveable.");
        }
        catch (Exception ex)
        {
            LogHelper.QueueLog(_logger, LogEntry.Types.Warning, ex.Message, ex);
        }
    }

    /// <summary>
    /// Coloca um item em sua posição inicial.
    /// </summary>
    /// <param name="placeable"> Item a ser colocado. </param>
    internal void Place(Placeable placeable)
    {
        if (_map[placeable.Position.y, placeable.Position.x] == null)
        {
            _map[placeable.Position.y, placeable.Position.x] = placeable;
            LogHelper.QueueLog(_logger, LogEntry.Types.Debug, $"Added {placeable.GetType()} to {placeable.Position}");
        }
        else
            LogHelper.QueueLog(_logger, LogEntry.Types.Warning, $"Position {placeable.Position} already occupied");
    }

    /// <summary>
    /// Exibe o mapa.
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
    /// Obtém a próxima posição, dada uma posição inicial e a direção a se mover.
    /// </summary>
    /// <param name="oldPosition"> Posição inicial. </param>
    /// <param name="direction"> Direção a se mover. </param>
    /// <returns> Próxima posição, caso a direção seja válida, posição inicial caso contrário. </returns>
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
    /// Verifica se determinada posição está sendo ocupada por outro item intransponível.
    /// </summary>
    /// <param name="position"> Posição a se verificar. </param>
    /// <returns> True caso esteja bloqueada, False caso contrário. </returns>
    private bool IsPositionBlocked(Position? position)
    {
        if (position == null)
            throw new NullReferenceException("Position is null");
        else
        {
            Position pos = (Position)position;
            return _map[pos.y, pos.x] != null && ((_map[pos.y, pos.x] is Obstacle obstacle && obstacle.Type != Obstacle.Types.Radioactive) || _map[pos.y, pos.x] is Jewel);
        }
    }

    /// <summary>
    /// Verifica se há um elemento radioativo em uma posição adjacente
    /// </summary>
    /// <returns> True caso possua, False caso contrário. </returns>
    private bool IsAdjacentToRadioactive(Position? position)
    {
        if (position == null)
            throw new NullReferenceException("Position is null");
        else
        {
            foreach (IMoveable.MoveDirections direction in Enum.GetValues(typeof(IMoveable.MoveDirections)).Cast<IMoveable.MoveDirections>())
            {
                Position pos = GetNextPosition((Position)position, direction);
                if (IsPositionValid(pos) && _map[pos.y, pos.x] != null && _map[pos.y, pos.x] is Obstacle obstacle && obstacle.Type == Obstacle.Types.Radioactive)
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Verifica se uma posição está dentro dos limites do mapa.
    /// </summary>
    /// <param name="position"> Posição a se verificar. </param>
    /// <returns> True caso seja válida, False caso contrário. </returns>
    private bool IsPositionValid(Position? position)
    {
        if (position == null)
            throw new NullReferenceException("Position is null");
        else
        {
            Position pos = (Position)position;
            return pos.y >= 0 && pos.y < _map.GetLength(0) && pos.x >= 0 && pos.x < _map.GetLength(1);
        }
    }
    #endregion Private Methods
}
