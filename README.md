# MEGA Configurador Promociones 2025

[![Angular](https://img.shields.io/badge/Angular-18-red?logo=angular)](https://angular.io/)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue?logo=.net)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-orange?logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.5-blue?logo=typescript)](https://www.typescriptlang.org/)
[![C#](https://img.shields.io/badge/C%23-12-green?logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)

----
## 📋 Tabla de Contenidos

 📖 Descripción del Proyecto
  
Sistema Integral de Gestión de Promociones y Cuentas de Suscriptores
Un proyecto desarrollado en colaboración entre MEGA y el proyecto de Ingenieros de Desarrollo 2025 de MEGA. La solución consiste en una plataforma centralizada para la configuración de promociones y el cálculo preciso de adeudos. Está diseñada específicamente para el personal de ventas y contratación, permitiéndoles gestionar ofertas comerciales y estados de cuenta de suscriptores de manera eficiente y sin errores.
👥 Participantes/Roles
  
-Gómez Medina Héctor Daniel.(Developer Full Stack)

-Hernández Camarena Aldo Kalid.(Developer Full Stack) 

-Aguilar Valle Marlene Esmeralda.(Designer UX/UI) 

-Castro Herrera Obed Esau.(Technical Writer(user guide)

📁 Estructura del Proyecto
Estructura General del Proyecto:
RetoMegaConfigurador2025/ 

├── 📁 client/          # Frontend (Angular 18) 

├── 📁 server/          # Backend (ASP.NET Core 8.0 con Web API) 

├── 📄 database.sql     # Script de base de datos 

├── 📄 package.json     # Configuración del proyecto general 

├── 📄 README.md        # Documentación 

└── 📄 RetoMegaConfigurador2025.sln  # Solución de Visual Studio )

client/ 

├── 📁 src/app/ 

│   ├── 📁 core/                    # Funcionalidades centrales 

│   ├── 📁 environments/            # Configuración de entornos 

│   ├── 📁 models/                  # Modelos de datos TypeScript 

│   │   ├── 📁 api/                # Interfaces para API 

│   │   └── 📄 *.model.ts          # Modelos de entidades 

│   ├── 📁 pages/                   # Páginas principales 

│   │   ├── 📁 debt-calculator/    # Calculadora de deuda 

│   │   ├── 📁 packages-services-manager/  # Gestión de paquetes 

│   │   ├── 📁 promotions-manager/ # Gestión de promociones 

│   │   └── 📁 subscribers-manager/ # Gestión de suscriptores 

│   ├── 📁 services/               # Servicios de Angular 

│   ├── 📁 shared/                 # Componentes compartidos 

│   │   ├── 📁 components/         # Componentes reutilizables 

│   │   ├── 📁 navbar/            # Barra de navegación 

│   │   └── 📁 objects/           # Objetos globales 

│   └── 📁 promociones/           # Componente de promociones 

├── 📄 angular.json               # Configuración de Angular 

├── 📄 package.json              # Dependencias y scripts 

└── 📄 tsconfig.json             # Configuración de TypeScript 

server/ 

├── 📁 controllers/              # Controladores de API 

│   ├── 📄 CCiudad.cs           # Controlador de ciudades 

│   ├── 📄 CColonia.cs          # Controlador de colonias 

│   ├── 📄 CDeuda.cs            # Controlador de deudas 

│   ├── 📄 CPaquete.cs          # Controlador de paquetes 

│   ├── 📄 CPromocion.cs        # Controlador de promociones 

│   ├── 📄 CSuscriptor.cs       # Controlador de suscriptores 

│   └── 📄 *.cs                 # Otros controladores 

├── 📁 data/                    # Contexto de base de datos 

│   ├── 📄 MEGADbContext.cs     # Contexto de Entity Framework 

│   └── 📁 objects/             # Objetos de datos extendidos 

├── 📁 interfaces/              # Interfaces/Contratos 

├── 📁 models/                  # Modelos de datos 

│   ├── 📁 DTOs/               # Data Transfer Objects 

│   └── 📄 *.cs                # Entidades del modelo 

├── 📁 repositories/           # Repositorios de datos 

│   └── 📄 Repo*.cs           # Implementaciones de repositorios 

├── 📄 Program.cs             # Punto de entrada de la aplicación 

├── 📄 megaapi.csproj        # Archivo de proyecto .NET 

└── 📄 deploy.sql            # Scripts de despliegue 

---

- ## 🛠️ Requerimientos Técnicos

### 💻 Software Requerido

| Software | Versión Mínima | Propósito |
|----------|----------------|----------|
| **Node.js** | 18.x | Runtime para Angular |
| **npm** | 9.x | Gestor de paquetes |
| **Angular CLI** | 18.x | Framework frontend |
| **.NET SDK** | 9.0 | Runtime backend |
| **SQL Server** | 2019+ | Base de datos |
| **Visual Studio** | 2022+ | IDE (opcional) |
| **VS Code** | Latest | Editor de código |
| **Git** | 2.x | Control de versiones |

### 🖥️ Requerimientos del Sistema
Este proyecto aún está en una etapa temprana de desarrollo. En futuras versiones se documentarán los requerimientos específicos del sistema mas en concreto.

- **OS**: Windows 10/11, macOS 10.15+, Ubuntu 20.04+
- **RAM**: 8GB mínimo (16GB recomendado)
- **Almacenamiento**: 2GB espacio libre
- **Procesador**: Intel i5 / AMD Ryzen 5 o superior
- 
---

## 📦 Tecnologías y Dependencias

### 🎨 Frontend (Angular 18)

```json
{
  "dependencies": {
    "@angular/animations": "^18.2.0",
    "@angular/common": "^18.2.0",
    "@angular/core": "^18.2.0",
    "@angular/forms": "^18.2.0",
    "@angular/router": "^18.2.0",
    "rxjs": "~7.8.0",
    "typescript": "~5.5.2"
  }
}
```

### ⚙️ Backend (ASP.NET Core 9.0)

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.5" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.5" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.6" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="9.0.1" />
```

### 🗄️ Base de Datos
- **SQL Server** con Entity Framework Core
- **Patrón Repository** para acceso a datos
- **Code First** con migraciones

---


## 🚀 Instalación y Configuración

### 1️⃣ Clonar el Repositorio

```bash
git clone https://github.com/tu-usuario/RetoMegaConfigurador2025.git
cd RetoMegaConfigurador2025
```

### 2️⃣ Configurar el Backend

#### Instalar dependencias .NET
```bash
cd server
dotnet restore
```

#### Crear archivos de configuración
Crea los archivos `appsettings.json` y `appsettings.Development.json` en la carpeta `server/`:

**appsettings.Development.json**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "db": "Server=.;Database=MEGAConfigurador;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"
  }
}
```

**appsettings.json**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "db": "Server=.;Database=MEGAConfigurador;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"
  }
}
```

**💡 Para SQL Server Authentication:**
```json
"db": "Server=.;Database=MEGAConfigurador;User Id=USUARIO;Password=CONTRASEÑA;TrustServerCertificate=True;"
```

### 3️⃣ Configurar el Frontend

#### Instalar dependencias Angular
```bash
cd ../client
npm install
```

#### Instalar Angular CLI (si no está instalado)
```bash
npm install -g @angular/cli@18
```

---


## 🗄️ Configuración de Base de Datos

### 1️⃣ Crear la Base de Datos

```bash
# Desde la carpeta server/
dotnet ef database update
```

### 2️⃣ Ejecutar Scripts Iniciales (Opcional)

```sql
-- Ejecutar database.sql en SQL Server Management Studio o mediante sqlcmd
sqlcmd -S . -d MEGAConfigurador -i database.sql
```

### 3️⃣ Verificar Conexión

```bash
# Probar conexión desde el backend
dotnet run --project server
```

---

## ▶️ Ejecutar la Aplicación

### 🔧 Modo Desarrollo

#### Terminal 1 - Backend
```bash
cd server
dotnet run
# API disponible en: https://localhost:7001
```

#### Terminal 2 - Frontend
```bash
cd client
ng serve
# App disponible en: http://localhost:4200
```

### 🚀 Modo Producción ( Aun en desarrollo, no disponible para produccion )

#### Compilar Frontend
```bash
cd client
ng build --configuration production
```

#### Compilar Backend
```bash
cd server
dotnet publish -c Release -o ./publish
```

---

## 📸 Capturas de Pantalla

### Navbar / seccion principal 
![Dashboard](./screenshots/dashboard.png)
*Vista principal del sistema con navegación y estadísticas*

### 🎯 Gestión de Promociones
![Promociones](./screenshots/promociones.png)
*Interfaz para crear y administrar promociones*

### 📦 Gestión de Paquetes
![Paquetes](./screenshots/paquetes.png)
*Módulo de administración de paquetes de servicios*

### 👥 Gestión de Suscriptores
![Suscriptores](./screenshots/suscriptores.png)
*Sistema de gestión de clientes y suscriptores*

### 💰 Calculadora de Deudas
![Calculadora](./screenshots/calculadora.png)
*Herramienta de cálculo de deudas y términos de pago*

---

## 🎨 Mockups y Diseño

> 📝 **Nota**: Incluir mockups y wireframes del diseño

### 🎯 Principios de Diseño
- **Responsive Design**: Adaptable a diferentes dispositivos
- **UX/UI Intuitivo**: Interfaz amigable para usuarios no técnicos
- **Consistencia Visual**: Paleta de colores y tipografía uniforme
- **Accesibilidad**: Cumplimiento de estándares WCAG

---

## 📊 Funcionalidades

### ✅ Funcionalidades Implementadas

| Módulo | Funcionalidad | Estado |
|--------|---------------|--------|
| 💰 **Deudas** | Calcular deudas con desgloce por periodo mensual.  | ✅ |
| 🎯 **Promociones** | CRUD promociones por tipo, con condiciones como la ubicacion, alcance de la promocion, duracion por meses y vigencia. | ✅ |
| 📦 **Paquetes** | Gestionar paquetes crear, modificar o eliminar con alcance del paquete, servicios incluidos y precio mensual. | ✅ |
| 👥 **Suscriptores** | Gestor suscriptores, muestra informacion completa del suscriptor y gestiona el contrato y el paquete puedes cancelar el paquete del suscriptor y asignarle otro. | ✅ |
| 🌍 **Ubicación** | Gestión ciudades/colonias , dependiendo de esto se le ofrecen servicios y promociones distintas. | ✅ |

### 🔄 Funcionalidades Futuras
- 📊 Dashboard con métricas
- 📈 Reportes avanzados

## 🔄 Metodología de Desarrollo

### 📋 Proceso de Desarrollo
1. **Análisis de Requerimientos**: Estudio del reto técnico.
2. **Diseño de Arquitectura**: Definición de estructura full-stack.
3. **Configuración del Entorno**: Setup de herramientas y dependencias
4. **Desarrollo del Backend**: API REST con .NET Core.
5. **Desarrollo del Frontend**: Interfaz con Angular.
6. **Integración**: Conexión frontend-backend.
7. **Testing**: Pruebas unitarias y de integración.
8. **Documentación**: README y comentarios de código.

### 🛠️ Herramientas Utilizadas
- **Control de Versiones**: Git + GitHub
- **IDE**: Visual Studio Code / Visual Studio
- **API Testing**: Postman
- **Base de Datos**: SQL Server Management Studio
- **Debugging**: DevTools del navegador

---

## 📈 Autoevaluación del Proyecto

### ✅ ¿Qué salió bien?

| Aspecto | Descripción | Puntuación |
|---------|-------------|------------|
| **🏗️ Arquitectura** | Separación clara frontend/backend, patrón Repository
| **🔧 Tecnologías** | Uso de tecnologías modernas (.NET 9, Angular 18) 
| **📱 Responsive** | Diseño adaptable a diferentes dispositivos 
| **🗄️ Base de Datos** | Modelo relacional bien estructurado 
| **📚 Documentación** | README completo y comentarios en código 
| **🔄 Git Flow** | Commits organizados y descriptivos

### ❌ ¿Qué no salió bien?

| Problema | Descripción | Impacto |
|----------|-------------|----------|
| **⏱️ Tiempo** | Gestión del tiempo en algunas funcionalidades
| **🧪 Testing** | Cobertura de pruebas unitarias limitada 
| **🎨 UI/UX** | Algunos componentes podrían ser más intuitivos 
| **📊 Validaciones** | Validaciones del frontend podrían ser más robustas 
| **🔒 Seguridad** | Falta implementar autenticación/autorización 

### 🚀 ¿Qué puedo mejorar?

| Área de Mejora | Propuesta | Prioridad |
|----------------|-----------|----------|
| **🧪 Testing** | Implementar pruebas unitarias.
| **⚡ Performance** | Optimizar queries y lazy loading.
| **🎨 UI/UX** | Mejorar animaciones y transiciones.

### 📝 Lecciones Aprendidas
- ✅ La planificación inicial es crucial para el éxito del proyecto, la gestion de ltiempo y asignacion de roles.
- ✅ La documentación continua facilita el desarrollo y leer documentacion, pedir ayuda al equipo.
- ✅ La comunicación entre frontend y backend debe estar bien definida.


<div align="center">

**🏢 Desarrollado para MEGA - Reto Técnico 2025**

*"Innovando en la gestión de promociones y servicios"*

[![Made with ❤️](https://img.shields.io/badge/Made%20with-❤️-red.svg)]()
[![Built with Angular](https://img.shields.io/badge/Built%20with-Angular-red?logo=angular)](https://angular.io/)
[![Powered by .NET](https://img.shields.io/badge/Powered%20by-.NET-blue?logo=.net)](https://dotnet.microsoft.com/)

</div>



