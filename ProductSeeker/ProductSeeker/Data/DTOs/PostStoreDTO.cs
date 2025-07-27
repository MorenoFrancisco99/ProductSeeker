namespace ProductSeeker.Data.DTOs
{
    /// <summary>
    /// DTO recieved from the client to create a new store.
    /// </summary>
    public class PostStoreDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string BussinessDays { get; set; } = string.Empty; //Dias de atencion, por ej. Lunes a Sabado
        public float Rating { get; set; } = 0.0f; //Rating del store, por ej. 4.5
        public string ExtraInfo { get; set; } = string.Empty; //Informacion extra del store, por ej. Horarios de atencion, etc.
        public string Img { get; set; } = string.Empty; //Imagen del store, por ej. Logo o foto del local
        public string Location { get; set; } = string.Empty; //Ubicacion del store, por ej. Ciudad, Provincia, etc.
        public string Address { get; set; } = string.Empty;
    }
}
