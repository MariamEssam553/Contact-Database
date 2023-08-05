CREATE MIGRATION m1fpjhsjpfhfph2k6t76i47l34yjwa22rxkgp4hepklry77ldcagoa
    ONTO initial
{
  CREATE TYPE default::Contact {
      CREATE PROPERTY birthDate: cal::local_date;
      CREATE PROPERTY description: std::str;
      CREATE PROPERTY email: std::str;
      CREATE PROPERTY firstName: std::str;
      CREATE PROPERTY secondName: std::str;
      CREATE PROPERTY status: std::bool;
  };
  CREATE SCALAR TYPE default::Title EXTENDING enum<Mr, Mrs, Ms>;
};
