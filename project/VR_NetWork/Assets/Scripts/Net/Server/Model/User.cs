

public enum Port
{
    Monitor,
    Operator,
    Roaming
}

public class Player
{
    public Player()
    {
        
    }

    public Player( string id , string port)
    {
        this.port = port;
        this.Id = id;
    }
    public string port{get;set;}/*0--> Monitor    1-->Operator      >2 --> 漫游端 */
    public string Id{get;set;}
}
public class RoleInfo
{
    public float postionX{get;set;}
    public float postionY{get;set;}
    public float postionZ{get;set;}
    public float rotationX{get;set;}
    public float rotationY{get;set;}
    public float rotationZ{get;set;}
    public string animator{get;set;}
}

