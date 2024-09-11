CREATE OR REPLACE FUNCTION sequencial_next_consultant(p_company_id BIGINT)
RETURNS TABLE (
    "Id" BIGINT,
    "FullName" varchar(255),
    "Active" BOOLEAN,
    "CompanyId" BIGINT,
    "TimelastLeadAssigned" TIMESTAMPTZ
) AS $$
BEGIN
    -- Primeira consulta
    RETURN QUERY
    SELECT 
		cs."Id",
		cs."FullName",
		cs."Active",
		cs."CompanyId",
		cs."TimeLastLeadAssigned"
    FROM "Consultant" cs
    WHERE cs."Active" = true
        AND cs."CompanyId" = p_company_id
        AND cs."TimeLastLeadAssigned" IS NULL;

    -- Verificar se a primeira consulta retornou resultados
    IF NOT FOUND THEN
        -- Segunda consulta se a primeira não retornar resultados
        RETURN QUERY
        SELECT
				cs."Id",
				cs."FullName",
				cs."Active",
				cs."CompanyId",
				cs."TimeLastLeadAssigned"
        FROM "Consultant" cs
        WHERE cs."Active" = true
            AND cs."CompanyId" = p_company_id
        ORDER BY cs."TimeLastLeadAssigned" ASC;
    END IF;
END;
$$ LANGUAGE plpgsql;