namespace BHTDS;

class BHTDSGame
{
    static void Main(string[] args)
    {
        using (BHTDSEngine engine = new BHTDSEngine())
        {
            engine.Run();
        }
    }
}