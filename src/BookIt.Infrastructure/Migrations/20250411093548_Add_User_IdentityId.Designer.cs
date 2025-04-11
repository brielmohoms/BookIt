﻿// <auto-generated />
using System;
using BookIt.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookIt.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250411093548_Add_User_IdentityId")]
    partial class Add_User_IdentityId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookIt.Domain.Apartments.Apartment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.PrimitiveCollection<int[]>("Amenities")
                        .IsRequired()
                        .HasColumnType("integer[]")
                        .HasColumnName("amenities");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<DateTime>("LastBookedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_booked_on_utc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_apartments");

                    b.ToTable("apartments", (string)null);
                });

            modelBuilder.Entity("BookIt.Domain.Bookings.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ApartmentId")
                        .HasColumnType("uuid")
                        .HasColumnName("apartment_id");

                    b.Property<DateTime>("DateCanceled")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_canceled");

                    b.Property<DateTime>("DateCompleted")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_completed");

                    b.Property<DateTime>("DateConfirmed")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_confirmed");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateRejected")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_rejected");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_bookings");

                    b.HasIndex("ApartmentId")
                        .HasDatabaseName("ix_bookings_apartment_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_bookings_user_id");

                    b.ToTable("bookings", (string)null);
                });

            modelBuilder.Entity("BookIt.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("identity_id");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_users_id");

                    b.ToTable("users", (string)null);
                });
            
            modelBuilder.Entity("BookIt.Domain.Reviews.Review", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasColumnName("id");

                b.Property<Guid>("ApartmentId")
                    .HasColumnType("uuid")
                    .HasColumnName("apartment_id");

                b.Property<Guid>("BookingId")
                    .HasColumnType("uuid")
                    .HasColumnName("booking_id");

                b.Property<string>("Comment")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("character varying(200)")
                    .HasColumnName("comment");

                b.Property<DateTime>("CreatedOnUtc")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_on_utc");

                b.Property<int>("Rating")
                    .HasColumnType("integer")
                    .HasColumnName("rating");

                b.Property<Guid>("UserId")
                    .HasColumnType("uuid")
                    .HasColumnName("user_id");

                b.HasKey("Id")
                    .HasName("pk_reviews");

                b.HasIndex("ApartmentId")
                    .HasDatabaseName("ix_reviews_apartment_id");

                b.HasIndex("BookingId")
                    .HasDatabaseName("ix_reviews_booking_id");

                b.HasIndex("UserId")
                    .HasDatabaseName("ix_reviews_user_id");

                b.ToTable("reviews", (string)null);
            });

            modelBuilder.Entity("BookIt.Domain.Apartments.Apartment", b =>
                {
                    b.OwnsOne("BookIt.Domain.Apartments.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("ApartmentId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address_city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address_country");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address_state");

                            b1.Property<string>("StreetNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address_street_number");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address_zip_code");

                            b1.HasKey("ApartmentId");

                            b1.ToTable("apartments");

                            b1.WithOwner()
                                .HasForeignKey("ApartmentId")
                                .HasConstraintName("fk_apartments_apartments_id");
                        });

                    b.OwnsOne("BookIt.Domain.Apartments.Money", "CleaningFee", b1 =>
                        {
                            b1.Property<Guid>("ApartmentId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("cleaning_fee_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("cleaning_fee_currency");

                            b1.HasKey("ApartmentId");

                            b1.ToTable("apartments");

                            b1.WithOwner()
                                .HasForeignKey("ApartmentId")
                                .HasConstraintName("fk_apartments_apartments_id");
                        });

                    b.OwnsOne("BookIt.Domain.Apartments.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("ApartmentId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("price_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("price_currency");

                            b1.HasKey("ApartmentId");

                            b1.ToTable("apartments");

                            b1.WithOwner()
                                .HasForeignKey("ApartmentId")
                                .HasConstraintName("fk_apartments_apartments_id");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("CleaningFee")
                        .IsRequired();

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("BookIt.Domain.Bookings.Booking", b =>
                {
                    b.HasOne("BookIt.Domain.Apartments.Apartment", null)
                        .WithMany()
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_bookings_apartments_apartment_id");

                    b.HasOne("BookIt.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_bookings_user_user_id");

                    b.OwnsOne("BookIt.Domain.Apartments.Money", "AmenitiesUpCharge", b1 =>
                        {
                            b1.Property<Guid>("BookingId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("amenities_up_charge_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("amenities_up_charge_currency");

                            b1.HasKey("BookingId");

                            b1.ToTable("bookings");

                            b1.WithOwner()
                                .HasForeignKey("BookingId")
                                .HasConstraintName("fk_bookings_bookings_id");
                        });

                    b.OwnsOne("BookIt.Domain.Apartments.Money", "CleaningFee", b1 =>
                        {
                            b1.Property<Guid>("BookingId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("cleaning_fee_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("cleaning_fee_currency");

                            b1.HasKey("BookingId");

                            b1.ToTable("bookings");

                            b1.WithOwner()
                                .HasForeignKey("BookingId")
                                .HasConstraintName("fk_bookings_bookings_id");
                        });

                    b.OwnsOne("BookIt.Domain.Apartments.Money", "PriceForPeriod", b1 =>
                        {
                            b1.Property<Guid>("BookingId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("price_for_period_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("price_for_period_currency");

                            b1.HasKey("BookingId");

                            b1.ToTable("bookings");

                            b1.WithOwner()
                                .HasForeignKey("BookingId")
                                .HasConstraintName("fk_bookings_bookings_id");
                        });

                    b.OwnsOne("BookIt.Domain.Apartments.Money", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("BookingId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("total_price_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("total_price_currency");

                            b1.HasKey("BookingId");

                            b1.ToTable("bookings");

                            b1.WithOwner()
                                .HasForeignKey("BookingId")
                                .HasConstraintName("fk_bookings_bookings_id");
                        });

                    b.OwnsOne("BookIt.Domain.DateRange", "Duration", b1 =>
                        {
                            b1.Property<Guid>("BookingId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateOnly>("EndDate")
                                .HasColumnType("date")
                                .HasColumnName("duration_end_date");

                            b1.Property<DateOnly>("StartDate")
                                .HasColumnType("date")
                                .HasColumnName("duration_start_date");

                            b1.HasKey("BookingId");

                            b1.ToTable("bookings");

                            b1.WithOwner()
                                .HasForeignKey("BookingId")
                                .HasConstraintName("fk_bookings_bookings_id");
                        });

                    b.Navigation("AmenitiesUpCharge")
                        .IsRequired();

                    b.Navigation("CleaningFee")
                        .IsRequired();

                    b.Navigation("Duration")
                        .IsRequired();

                    b.Navigation("PriceForPeriod")
                        .IsRequired();

                    b.Navigation("TotalPrice")
                        .IsRequired();
                });
            
            modelBuilder.Entity("BookIt.Domain.Reviews.Review", b =>
            {
                b.HasOne("BookIt.Domain.Apartments.Apartment", null)
                    .WithMany()
                    .HasForeignKey("ApartmentId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("fk_reviews_apartments_apartment_id");

                b.HasOne("BookIt.Domain.Bookings.Booking", null)
                    .WithMany()
                    .HasForeignKey("BookingId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("fk_reviews_bookings_booking_id");

                b.HasOne("BookIt.Domain.Users.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("fk_reviews_user_user_id");
            });
#pragma warning restore 612, 618
        }
    }
}
