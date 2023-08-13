CREATE MIGRATION m1qrdnxcgcga3v2vvvhbdpyajcn7v7oqx7phmz2f62nnqds7p43cia
    ONTO m1f4j4g4rthudngdiok6q3wi336hxjmlppdtfywimwqvr7oay5i2ea
{
  CREATE SCALAR TYPE default::Role EXTENDING enum<Normal, Admin>;
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY contactRole: default::Role {
          SET REQUIRED USING (<default::Role>{});
      };
      CREATE REQUIRED PROPERTY password: std::str {
          SET REQUIRED USING (<std::str>{});
      };
      CREATE REQUIRED PROPERTY username: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
};
