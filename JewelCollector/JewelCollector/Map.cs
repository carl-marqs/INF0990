namespace JewelCollector;

/// <summary>
/// Representa o mapa 2D do jogo.
/// </summary>
internal static class Map
{
    #region Fields
    private static Placeable[,] _map;
    #endregion Fields    

    #region Visible Methods
    /// <summary>
    /// Cria um novo mapa dado a largura e altura.
    /// </summary>
    /// <param name="width"> Largura do mapa a ser criado. </param>
    /// <param name="height"> Altura do mapa a ser criado. </param>
    internal static void CreateMap(byte width, byte height)
    {
        _map = new Placeable[width, height];
    }
    #endregion Visible Methods
}
