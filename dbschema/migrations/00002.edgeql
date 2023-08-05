CREATE MIGRATION m1vnqg7xzu7dqn2j36wkhpbp2nizg2cavaos2izuds6oywmilfo2ea
    ONTO m1fpjhsjpfhfph2k6t76i47l34yjwa22rxkgp4hepklry77ldcagoa
{
  ALTER TYPE default::Contact {
      ALTER PROPERTY birthDate {
          SET REQUIRED USING (<cal::local_date>{});
      };
      ALTER PROPERTY email {
          SET REQUIRED USING (<std::str>{});
      };
      ALTER PROPERTY firstName {
          SET REQUIRED USING (<std::str>{});
      };
      ALTER PROPERTY secondName {
          SET REQUIRED USING (<std::str>{});
      };
      ALTER PROPERTY status {
          SET REQUIRED USING (<std::bool>{});
      };
  };
};
