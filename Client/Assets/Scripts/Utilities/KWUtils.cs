using UnityEngine;
using System.Collections;

public class KWUtils
{
    /// <summary>
    /// 字符串转16进制，mark dong
    /// </summary>
    /// <param name="ss"></param>
    public static int String2Hash(string ss)
    {
        char[] chArray = ss.ToCharArray();
        uint num = 0;
        uint num2 = 0;
        uint num3 = 0;
        uint num4 = 0x83;           //131

        while(num3 < chArray.Length)
        {
            num = ((num << 4) * num4) + chArray[num3++];
            num2 = num & 0xf0000000;
            if (num2 != 0)
            {
                num ^= num2 >> 0x18;
                num &= num2;
            }
        }
        return (((int)num) & 0x7fffffff);
    }
}
