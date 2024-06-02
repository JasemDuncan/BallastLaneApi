CREATE TABLE project
(
    project_id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    cost DECIMAL,
    sales INT
);

CREATE TABLE "user"
(
    user_id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    lastname VARCHAR(50) NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    salt VARCHAR(255) NOT NULL
);


INSERT INTO project
    (name, description, cost, sales)
VALUES
    ('Annise', 'An extensive, multi-year team effort dedicated to crafting a platform designed for the collection, management, and meaningful presentation of vast data sets. This platform audits, empowers and enables family offices in effectively managing their investments, providing valuable insights and tools.', 10000, 100),
    ('PrescriberPoint', 'Ballast Lane Applications assembled a dedicated team to successfully bring the minimum viable product to its final form. We remain committed to ongoing development of new and existing features.', 20000, 200),
    ('Reibus', 'To help accelerate their efforts, Reibus engaged our design, architecture, and development teams. We provided the foundation for their core web and mobile development engineering capability, and our team works shoulder to shoulder with client leadership on the product and technical side.', 30000, 300),
    ('Holcim', 'Ballast Lane provided design and business analyst expertise to design features to enable stakeholders to be more efficient and smarter, and to accelerate the completion of roadmap items.', 40000, 400),
    ('Sage', 'Our team worked closely with the founder, to evaluate existing health and wellness apps and validate hypotheses regarding market gaps. We built an iOS app for beta testing using the insights and findings from our discovery phase.', 50000, 500),
    ('Inturn', 'The client engaged Ballast Lane resources to work shoulder to shoulder with their teams to provide core engineering and QA resources for their PHP-based platform.', 60000, 600),
    ('Get Your Nest', 'Comprehensive real estate mobile app, client website and admin portal, designed to streamline the process of buying property.', 70000, 700),
    ('PERQ', 'Ballast Lane Applications was engaged to provide expertise in developing, maintaining, and testing the current and enhanced features of a Marketing AI tool tailored for Leasing and Real Estate companies.', 80000, 800);


INSERT INTO "user"
    (name, lastname, username, password, email, salt)
VALUES
    ('Carleto', 'Anchelotti', 'ballastadmin', '$2a$11$3EP8kRFJvzQyN7SFjju6yOaU2QKYy7Nf7WMwdzJDtMX9z7Z/0j6cm', 'user@ballast.com', '$2a$11$3EP8kRFJvzQyN7SFjju6yO'),
    ('Gussepi', 'Richett', 'ballastmanager', '$2a$11$R0PjlemJhsDCvOPycHjsTeq1JIBbiAcrNxRhZP.7xy7h/73TkPAJy', 'manager@ballast.com', '$2a$11$R0PjlemJhsDCvOPycHjsTe');