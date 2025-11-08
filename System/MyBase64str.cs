using System;
using System.Text;

public class MyBase64str
{
    private Encoding enc;

    public MyBase64str(string encStr)
    {
        enc = Encoding.GetEncoding(encStr);
    }
    /// <summary>
    /// 暗号化する
    /// </summary>
    /// <param name="str">暗号化したいもの</param>
    /// <returns></returns>
    public string Encode(string str)
    {
        return Convert.ToBase64String(enc.GetBytes(str));
    }
    /// <summary>
    /// エンコードしたものをデコードする
    /// </summary>
    /// <param name="str">デコードする文字</param>
    /// <returns></returns>
    public string Decode(string str)
    {
        return enc.GetString(Convert.FromBase64String(str));
    }
}