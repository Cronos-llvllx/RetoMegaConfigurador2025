using megaapi.Interfaces;
using megaapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace megaapi.Repositories
{
    public class SubscriberRepository : ISubscriberRepository
    {
        // SIMULACIÓN DE LA BASE DE DATOS EN MEMORIA
        private static readonly List<Subscriber> _subscribers = new List<Subscriber>();
        private static int _nextId = 1; // Para simular el auto-incremento (IDENTITY)

        public SubscriberRepository()
        {
            // Opcional: inicializa algunos datos de prueba si la lista está vacía
            if (!_subscribers.Any())
            {
                _subscribers.Add(new Subscriber
                {
                    IdSubscriber = _nextId++,
                    Name = "Juan Sánchez Torres",
                    Email = "juan@ejem.com",
                    PhoneNumber = "3311223344",
                    IdNeighborhood = 1,
                    Type = 1
                });
                _subscribers.Add(new Subscriber
                {
                    IdSubscriber = _nextId++,
                    Name = "Empresa ABC",
                    Email = "contacto@abc.com",
                    PhoneNumber = "5512345678",
                    IdNeighborhood = 2,
                    Type = 2
                });
            }
        }

        // ----------------------------------------------------------------------
        // IMPLEMENTACIÓN DE LOS MÉTODOS DE LA INTERFAZ
        // ----------------------------------------------------------------------

        // Implementación del método AddSubscriber (nombre corregido)
        public void AddSubscriber(Subscriber subscriber)
        {
            if (subscriber.IdSubscriber == 0)
            {
                subscriber.IdSubscriber = _nextId++;
            }
            _subscribers.Add(subscriber);
        }

        // Implementación del método GetAllSubscribers (nombre corregido)
        public IEnumerable<Subscriber> GetAllSubscribers()
        {
            return _subscribers;
        }

        // Este método ya estaba bien
        public Subscriber? GetSubscriberById(int id)
        {
            return _subscribers.FirstOrDefault(s => s.IdSubscriber == id);
        }

        // Este método ya estaba bien
        public void UpdateSubscriber(Subscriber subscriber)
        {
            var existingSubscriber = _subscribers.FirstOrDefault(s => s.IdSubscriber == subscriber.IdSubscriber);

            if (existingSubscriber != null)
            {
                existingSubscriber.Name = subscriber.Name;
                existingSubscriber.Email = subscriber.Email;
                existingSubscriber.PhoneNumber = subscriber.PhoneNumber;
                existingSubscriber.IdNeighborhood = subscriber.IdNeighborhood;
                existingSubscriber.Type = subscriber.Type;
            }
        }

        // Este método ya estaba bien
        public void SaveChanges()
        {
            // No hace nada en esta versión en memoria
        }
    }
}