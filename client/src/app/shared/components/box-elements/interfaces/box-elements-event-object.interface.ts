interface BoxElementsEventObject {
  /** El identificador del componente destinatario o remitente del evento. */
  componentId: string,
  /** El elemento que disparó el evento. */
  element: string,
  /** Indica el tipo de evento. */
  eventType: 'add' | 'remove'
}

export default BoxElementsEventObject;