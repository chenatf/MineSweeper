using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweep.Model;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace Test
{

    class Program
    {
        static string Print(MineField field)
        {
            var builder = new StringBuilder();
            for(int i = 0; i < field.Width; ++i)
            {
                for(int j = 0; j < field.Height; ++j)
                {
                    var x = field[i, j];
                    switch(x)
                    {
                    case -1:
                        builder.Append('*');
                        break;
                    case 0:
                        builder.Append(' ');
                        break;
                    case int count:
                        builder.Append(count);
                        break;
                    }
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
        static void Main(string[] args)
        {
            var field = new MineField(9, 9, 10);
            Console.WriteLine(Print(field));
        }
    }
}
