namespace App.Entities
{
    /// <summary>
    /// Clase que contiene la respuesta retornada de una operación
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Obtiene o establece el estado de la operación
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Obtiene o establece un error ocurrido en la operación
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Obtiene o establece, el resultado,si la operación fue exitosa
        /// </summary>
        public object Result { get; set; }
    }
}
