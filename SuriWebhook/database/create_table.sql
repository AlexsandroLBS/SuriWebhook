DO $$ 
BEGIN 
    IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'boletos') THEN 
        CREATE TABLE boletos (
            id BIGSERIAL NOT NULL PRIMARY KEY,
            cpf VARCHAR(11) UNIQUE,
            content TEXT
        );
    END IF; 
END $$;
