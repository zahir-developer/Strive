CREATE SCHEMA [StriveSuperAdminTest]
    AUTHORIZATION [dbo];


GO
GRANT UPDATE
    ON SCHEMA::[StriveSuperAdminTest] TO [StriveSuperAdminTestuser];


GO
GRANT SELECT
    ON SCHEMA::[StriveSuperAdminTest] TO [StriveSuperAdminTestuser];


GO
GRANT INSERT
    ON SCHEMA::[StriveSuperAdminTest] TO [StriveSuperAdminTestuser];


GO
GRANT EXECUTE
    ON SCHEMA::[StriveSuperAdminTest] TO [StriveSuperAdminTestuser];


GO
GRANT DELETE
    ON SCHEMA::[StriveSuperAdminTest] TO [StriveSuperAdminTestuser];


GO
GRANT CREATE SEQUENCE
    ON SCHEMA::[StriveSuperAdminTest] TO [StriveSuperAdminTestuser];


GO
GRANT ALTER
    ON SCHEMA::[StriveSuperAdminTest] TO [StriveSuperAdminTestuser];

