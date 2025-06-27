using megaapi.Models; // Asegúrate de que el namespace sea correcto
namespace megaapi.Interfaces
{
    public interface ISubscriberRepository
    {
        // Método para obtener un suscriptor por su ID
        Subscriber? GetSubscriberById(int id);

        // Método para obtener todos los suscriptores (ej. para un listado)
        IEnumerable<Subscriber> GetAllSubscribers();

        // Método para agregar un nuevo suscriptor
        void AddSubscriber(Subscriber subscriber);

        // Método para actualizar la información de un suscriptor existente
        void UpdateSubscriber(Subscriber subscriber);

        // Método para guardar los cambios en la base de datos
        void SaveChanges();
    }
} 