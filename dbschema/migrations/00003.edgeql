CREATE MIGRATION m13xf6xqyotqoyupxtcnceesracgj4xxzcvtyaziza6fxmkhfbrawq
    ONTO m1vnqg7xzu7dqn2j36wkhpbp2nizg2cavaos2izuds6oywmilfo2ea
{
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY title: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
  DROP SCALAR TYPE default::Title;
};
