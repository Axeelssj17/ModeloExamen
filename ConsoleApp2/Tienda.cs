using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Tienda
    {
        public string Nombre { get; set; }
        public List<Producto> Productos { get; set; } = new List<Producto>();
        public List<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();

    }
}
