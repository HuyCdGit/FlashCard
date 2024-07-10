from faker import Faker
from connect_db import connect_to_database

# Tạo danh sách dữ liệu giả ngẫu nhiên
fake = Faker()

def generate_fake_data():
    random_username = fake.user_name()
    random_password = fake.password(length=10)
    random_email = fake.email()
    random_phone = fake.numerify(text='0##########')
    random_full_name = fake.name()
    random_date_of_birth = fake.date_of_birth(minimum_age=18, maximum_age=50)
    random_country = fake.country()

    return (random_username, random_password, random_email, random_phone, random_full_name, random_date_of_birth, random_country)

def insert_fake_users(conn):
    cursor = conn.cursor()
    fake_users = [generate_fake_data() for _ in range(10)]

    for user in fake_users:
        cursor.execute('''
            INSERT INTO users (username, hashed_password, email, phone, full_name, date_of_birth, country)
            VALUES (?, ?, ?, ?, ?, ?, ?)
        ''', user)

    conn.commit()
    print("Inserted fake records into the Users table.")

if __name__ == "__main__":
    connection = connect_to_database()
    insert_fake_users(connection)
    connection.close()
