using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using ConsoleApp2;

Tienda tienda = new Tienda();
Console.WriteLine("Ingrese el nombre de la tienda:");
tienda.Nombre = Console.ReadLine();


while (true)
{
    MostrarMenu();
    int opcion = int.Parse(Console.ReadLine());

    switch (opcion)
    {
        case 1: AgregarProducto(); break;
        case 2: RegistrarCompra(); break;
        case 3: RegistrarVenta(); break;
        case 4: MostrarStockProductos(); break;
        case 5: MostrarTotalCompras(); break;
        case 6: MostrarTotalVentas(); break;
        case 7: Environment.Exit(0); break;
        default: Console.WriteLine("Opción inválida."); break;
    }

    Console.WriteLine("Presione una tecla para continuar");
    Console.ReadKey();
}
void MostrarMenu()
    {
        Console.WriteLine("Menú de la tienda ");

        Console.WriteLine("1. Agregar Producto");

        Console.WriteLine("2. Registrar Compra");

        Console.WriteLine("3. Registrar Venta");

        Console.WriteLine("4. Mostrar stock de productos");

        Console.WriteLine("5. Mostrar total de ventas");

        Console.WriteLine("6. Mostrar total de compras");

        Console.WriteLine("6. Salir");
        Console.Write("Seleccione una opción: ");
    }


//1
void AgregarProducto()
{
    Producto p = new Producto();
    Console.Write("Ingrese el código del producto: ");
    string input = Console.ReadLine()?.Trim();

    if (!int.TryParse(input, out int codigo))
    {
        Console.WriteLine("Código inválido. Debe ser un número entero.");
        return;
    }

    bool Existe(int c) => tienda.Productos.Any(prod => prod.Codigo == c);

    if (Existe(codigo))
    {
        Console.WriteLine("El código del producto ya existe. Intente con otro código.");
        return;
    }

    p.Codigo = codigo;

    Console.Write("Ingrese la descripción del producto: ");
    p.Descripcion = Console.ReadLine();

    Console.Write("Ingrese el stock inicial del producto: ");
    if (!double.TryParse(Console.ReadLine(), out double stock))
    {
        Console.WriteLine("Stock inválido.");
        return;
    }
    p.Stock = stock;

    tienda.Productos.Add(p);
    Console.WriteLine("Producto agregado exitosamente.");
}


//2
void RegistrarCompra()
{
    Compra c = new Compra();
    Console.Write("Ingrese el código del producto: ");
    c.CodigoProducto = int.Parse(Console.ReadLine());
    Console.Write("Ingrese la cantidad comprada: ");
    c.Cantidad = int.Parse(Console.ReadLine());
    Console.Write("Ingrese el precio unitario: ");
    c.PrecioUnitario = double.Parse(Console.ReadLine());
    Console.Write("Ingrese el nombre del proveedor: ");
    c.Proveedor = Console.ReadLine();
    tienda.Comprobantes.Add(c);
    Producto producto = tienda.Productos.Find(p => p.Codigo == c.CodigoProducto);
    if (producto != null)
    {
        producto.Stock += c.Cantidad;
        Console.WriteLine("Compra registrada exitosamente.");
    }
    else
    {
        Console.WriteLine("Producto no encontrado.");
    }
}


//3
void RegistrarVenta()
{
    Comprobante venta = new Comprobante();
    Console.Write("Ingrese el código del producto: ");
    venta.CodigoProducto = int.Parse(Console.ReadLine());
    Console.Write("Ingrese la cantidad vendida: ");
    venta.Cantidad = int.Parse(Console.ReadLine());
    Console.Write("Ingrese el precio unitario: ");
    venta.PrecioUnitario = double.Parse(Console.ReadLine());
    tienda.Comprobantes.Add(venta);
    Producto producto = tienda.Productos.Find(p => p.Codigo == venta.CodigoProducto);
    if (producto != null)
    {
        if (producto.Stock >= venta.Cantidad)
        {
            producto.Stock -= venta.Cantidad;
            Console.WriteLine("Venta registrada exitosamente.");
        }
        else
        {
            Console.WriteLine("Stock insuficiente para realizar la venta.");
        }
    }
    else
    {
        Console.WriteLine("Producto no encontrado.");
    }
}


//4
void MostrarStockProductos()
{
    Console.WriteLine("Stock de Productos:");
    foreach (var producto in tienda.Productos)
    {
        Console.WriteLine($"Código: {producto.Codigo}, Descripción: {producto.Descripcion}, Stock: {producto.Stock}");
    }
}


//5
void MostrarTotalVentas()
{
    double totalVentas = 0;
    foreach (var comprobante in tienda.Comprobantes)
    {
        if (comprobante is Comprobante)
        {
            totalVentas += comprobante.Cantidad * comprobante.PrecioUnitario;
        }
    }
    Console.WriteLine($"Total de Ventas: {totalVentas}");
}


//6
void MostrarTotalCompras()
{
    double totalCompras = 0;
    foreach (var comprobante in tienda.Comprobantes)
    {
        if (comprobante is Compra)
        {
            totalCompras += comprobante.Cantidad * comprobante.PrecioUnitario;
        }
    }
    Console.WriteLine($"Total de Compras: {totalCompras}");
}