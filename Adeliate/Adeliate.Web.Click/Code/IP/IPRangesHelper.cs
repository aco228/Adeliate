using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Adeliate.Web.Click.Code.IP
{
  public class IPRangesHelper
  {

    public static bool IsInTheRange(string iprange, string address)
    {
      string[] splitBits = address.Split('/');
      if (splitBits.Length != 2)
        return false;

      int bits;
      if (!Int32.TryParse(splitBits[1], out bits))
        return false;
      
      IPAddress ip;
      if (!IPAddress.TryParse(splitBits[0], out ip))
        return false;

      uint mask = ~(uint.MaxValue >> bits);

      // Convert the IP address to bytes.
      byte[] ipBytes = ip.GetAddressBytes();

      // BitConverter gives bytes in opposite order to GetAddressBytes().
      byte[] maskBytes = BitConverter.GetBytes(mask).Reverse().ToArray();

      byte[] startIPBytes = new byte[ipBytes.Length];
      byte[] endIPBytes = new byte[ipBytes.Length];

      // Calculate the bytes of the start and end IP addresses.
      for (int i = 0; i < ipBytes.Length; i++)
      {
        startIPBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
        endIPBytes[i] = (byte)(ipBytes[i] | ~maskBytes[i]);
      }

      // Convert the bytes to IP addresses.
      IPAddress startIP = new IPAddress(startIPBytes);
      IPAddress endIP = new IPAddress(endIPBytes);

      return IPRangesHelper.IsInRange(startIP, endIP, address);
    }
    
    private static bool IsInRange(IPAddress startIpAddr, IPAddress endIpAddr, string address)
    {
      long ipStart = BitConverter.ToInt32(startIpAddr.GetAddressBytes().Reverse().ToArray(), 0);
      long ipEnd = BitConverter.ToInt32(endIpAddr.GetAddressBytes().Reverse().ToArray(), 0);
      long ip = BitConverter.ToInt32(IPAddress.Parse(address).GetAddressBytes().Reverse().ToArray(), 0);
      return ip >= ipStart && ip <= ipEnd; //edited
    }

  }
}