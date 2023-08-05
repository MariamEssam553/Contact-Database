CREATE MIGRATION m1co53depdao4bkrdcnnf6xny3oaq5oqk35qthucaj3zejruvxtxna
    ONTO m13xf6xqyotqoyupxtcnceesracgj4xxzcvtyaziza6fxmkhfbrawq
{
  ALTER TYPE default::Contact {
      ALTER PROPERTY secondName {
          RENAME TO lastName;
      };
  };
};
