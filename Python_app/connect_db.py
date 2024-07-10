import pyodbc

def connect_to_database():
    server_name = 'localhost'
    database_name = 'Clone_Quizizz_db'
    port = '1434'
    username = 'sa'
    password = 'Password123@'
    driver = '{ODBC Driver 18 for SQL Server}'

    Connection_string = ('DRIVER={driver};'
                         'SERVER={0},{1};'
                         'DATABASE={2};'
                         'UID={3};'
                         'PWD={4};TrustServerCertificate=Yes').format(server_name,
                                                                      port,
                                                                      database_name,
                                                                      username,
                                                                      password,
                                                                      driver=driver)

    conn = pyodbc.connect(Connection_string)
    return conn

if __name__ == "__main__":
    connection = connect_to_database()
    print("Connected to the database successfully!")
