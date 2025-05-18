namespace cope.Relic.RelicChunky.ChunkTypes.GameDataChunk
{
    public interface IRGDKeyConverter
    {
        string HashToKey(ulong hash);
        ulong KeyToHash(string key);
    }
}