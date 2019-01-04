namespace Common
{
    public enum ActionCode{
        Login,
        Logout,
        Task,
        Move,
        MoveResponse,
        ObjectMove,
        GetPlayer,
        Pointer,
        None,
    }

    public enum RequestCode{
        Player,
        Main,
        Game,
        None
    }

    public enum ReturnCode{
        Success,
        Fail,
        NotFound
    }
  
}