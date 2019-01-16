namespace Common
{
    public enum ActionCode{
        Login, //登陆
        Logout, //登出
        Task, //任务
        Move, //角色移动
        MoveResponse,//角色移动响应
        ObjectMove,//对象移动
        GetPlayer,//获取在线人数
        Pointer, //射线点击
        Simple,//简单的任意时间（转发功能）
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