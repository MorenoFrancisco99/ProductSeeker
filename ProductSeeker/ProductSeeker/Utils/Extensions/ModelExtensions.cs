using ProductSeeker;
using ProductSeeker.Data.Models;

public static class ModelDTOExtension
{

    
    public static List<object> GetIdentifier(this ProductSpecModel spec)
    {
        //Fragil
        //Hy que considerar el orden de los elementos en el DTO y el modelo, y agregar un case por cada tipo de producto
        //Quedan pocas opciones que no sean agregar metodos al modelo, o repetir codigo en el repositorio
        switch (spec)
        {
            case FoodProductModel foodSpec:
                return new List<object> { foodSpec.NetContent, foodSpec.UnitOfMeasure };
            // Agregar casos para otros tipos de productos según sea necesario
            default:
                throw new ArgumentException("Tipo de especificación no reconocido");
        }
    }
}