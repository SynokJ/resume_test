public class Node
{
    public GameEnumerators.NodeType Type { get => _type; }
    private GameEnumerators.NodeType _type;

    public Node(GameEnumerators.NodeType type)
    {
        _type = type;
    }

    ~Node()
    {
    }

    public override string ToString()
    {
        string res = "/";

        switch (_type)
        {
            case GameEnumerators.NodeType.Walckable: res = "*"; break;
            case GameEnumerators.NodeType.VerWall: res = "|"; break;
            case GameEnumerators.NodeType.HorWall: res = "-"; break;
        }

        return res;
    }
}