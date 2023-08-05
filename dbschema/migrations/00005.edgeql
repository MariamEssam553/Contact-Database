CREATE MIGRATION m1f4j4g4rthudngdiok6q3wi336hxjmlppdtfywimwqvr7oay5i2ea
    ONTO m1co53depdao4bkrdcnnf6xny3oaq5oqk35qthucaj3zejruvxtxna
{
  ALTER TYPE default::Contact {
      ALTER PROPERTY birthDate {
          SET TYPE std::str USING (<std::str>.birthDate);
      };
  };
};
