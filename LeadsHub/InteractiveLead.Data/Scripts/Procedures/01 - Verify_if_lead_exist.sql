
CREATE OR REPLACE FUNCTION lead_exists(
    p_companyid BIGINT,
    p_phonenumber VARCHAR
)
RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    lead_count INT;
BEGIN
    SELECT COUNT(*) INTO lead_count
    FROM lead
    WHERE companyid = p_companyid AND phonenumber = p_phonenumber;
    
    IF lead_count > 0 THEN
        RETURN TRUE;
    ELSE
        RETURN FALSE;
    END IF;
END;
$$;
