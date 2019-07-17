using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adlite.IPInserter.Models
{
  public class MobileOperatorModel
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public List<MccMncCombination> MncMccCombination { get; set; }

  }
}
