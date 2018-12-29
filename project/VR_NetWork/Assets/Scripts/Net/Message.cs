using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

public  class Message
{
    private byte[] data = new byte[1024];
    private int startIndex = 0;

    public byte[] Data
    {
        get { return data; }
    }
    public int StartIndex
    {
        get { return startIndex; }
    }
    public int RemainSize
    {
        get { return data.Length - startIndex; }
    }
    /// <summary>
    /// 解析数据或者叫做读取数据
    /// </summary>
    public void ReadMessage(int newDataAmount, Action<RequestCode,ActionCode,string> processDataCallback )
    {
        startIndex += newDataAmount;
        while (true)
        {
            if (startIndex <= 4) return;
            int count = BitConverter.ToInt32(data, 0);
            if ((startIndex - 4) >= count)
            {
                RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
                string s = Encoding.UTF8.GetString(data, 12, count-8);
                processDataCallback(requestCode, actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= (count + 4);
            }
            else
            {
                break;
            }
        }
    }
    
    public void ClientReadMessages(int newDataAmount, Action<ActionCode, string> processDataCallback)
    {
        startIndex += newDataAmount;
        while (true)
        {
            if (startIndex <= 4) return;
            int count = BitConverter.ToInt32(data, 0);
            if ((startIndex - 4) >= count)
            {
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                string s = Encoding.UTF8.GetString(data, 8, count - 4);
                processDataCallback(actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= (count + 4);
            }
            else
            {
                break;
            }
        }
    }
    public static byte[] PackData(ActionCode actionCode,string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        byte[] newBytes =dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>();
        return newBytes.Concat(dataBytes).ToArray<byte>();
    }

     public static byte[] ClientPackData(RequestCode requestData,ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestData);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + dataBytes.Length + actionCodeBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>()
            .Concat(actionCodeBytes).ToArray<byte>()
            .Concat(dataBytes).ToArray<byte>();
    }
}
