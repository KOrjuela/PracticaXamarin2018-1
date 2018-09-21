namespace Utility
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Clase que contiene tareas genericas de los ViewModel
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Representa el método [PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)]
        /// que controlará al evento PropertyChanged que se provoque cuando cambie una propiedad en un componente.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Método que actualiza las propiedades "OneWay" or "TwoWay" reflejando en el destino
        /// automaticamente los cambios dinamicos del origen
        /// </summary>
        /// <param name="propertyName"> Método o nombre de la propiedad que llama al método UpdateValueProperty</param>
        /// <Remember>
        /// <[CallerMemberName]>
        /// Permite obtener el método o el nombre de la propiedad de la persona que llama al método.
        /// </Remember>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            /// invoke method PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Método que actualiza el valor de una propiedad, si este ha cambiado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto</typeparam>
        /// <param name="property">Valor base de la Propiedad</param>
        /// <param name="value">Nuevo valor</param>
        /// <param name="propertyName"> Método o nombre de la propiedad que llama al método UpdateValueProperty</param>
        /// <Remember>
        /// <[CallerMemberName]>
        /// Permite obtener el método o el nombre de la propiedad de la persona que llama al método.
        /// <[EqualityComparer]>
        /// Implementación de IEqualityComparer<T>. Valida si dos objetos son o no iguales 
        /// </Remember>
        protected void UpdateValueProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(property, value))
            {
                return;
            }

            property = value;
            OnPropertyChanged(propertyName);
        }

    }
}
