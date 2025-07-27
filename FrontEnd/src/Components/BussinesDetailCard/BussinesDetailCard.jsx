import './BussinesDetailCard.css';

function BussinesDetailCard({ commerce }) {
  if (!commerce) {
    return <p>No hay datos</p>;
  }
  //TODO se deberia llamar al back para el promedio de precios y el rating promedio

  return (
    <div className="inspect-container">
      <div className="inspect-header">
        <h2>{commerce.Nombre}</h2>
        <h3>{commerce.Tipo}</h3>
      </div>
      <div className="inspect-content">
        <div className="inspect-photo">
          <img
            src={commerce.Img || "/images/placeholder2.png"}
            alt={commerce.Nombre}
          />
        </div>
        <div className="inspect-info">
          <p><strong>Cantidad de productos registrados:</strong> {commerce.QuantityOfProducts || 'N/A'}</p>
          <p><strong>Horarios de atención:</strong> {commerce.BussinesDays || 'N/A'}</p>
          <p><strong>Promedio de precios:</strong> {commerce.AveragePrice || 'N/A'}</p>
          <p><strong>Rating promedio de otros usuarios:</strong> {commerce.Rating || 'N/A'}</p>
          <p><strong>Info extra:</strong> {commerce.ExtraInfo || 'N/A'}</p>
          <div className="inspect-links">
            <span className="nav-button">[Agregar a mis negocios]</span>
            <span className="nav-button">[Ver ubicación]</span>
            <span className="nav-button">[Ver productos de otros usuarios]</span>
          </div>
          <div className="inspect-actions">
            <button className="nav-button">[Editar]</button>
            <button className="nav-button">[Eliminar]</button>
            <button className="nav-button">[Volver]</button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default BussinesDetailCard;
