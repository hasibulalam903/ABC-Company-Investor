<%@ Page Title="About Us" Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="About.aspx.vb"
    Inherits="About" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style type="text/css">

        /* ==========================================
           ABOUT PAGE
           ========================================== */

        .about-page {
            min-height: 100vh;
            background: #f5f7fb;
            font-family: Arial, Helvetica, sans-serif;
        }

        /* ==========================================
           HERO
           ========================================== */

        .about-hero {
            background: linear-gradient(
                135deg,
                #0f172a,
                #0f766e
            );

            padding: 90px 20px;
            text-align: center;
            color: white;
        }

        .about-hero h1 {
            margin: 0 0 18px;
            font-size: 48px;
            font-weight: 700;
        }

        .about-hero p {
            max-width: 800px;
            margin: 0 auto;
            font-size: 18px;
            line-height: 1.8;
            color: #e2e8f0;
        }

        /* ==========================================
           INTRO SECTION
           ========================================== */

        .about-container {
            max-width: 1200px;
            margin: auto;
            padding: 70px 25px;
        }

        .about-intro {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 50px;
            align-items: center;
            margin-bottom: 70px;
        }

        .about-intro h2 {
            font-size: 34px;
            color: #0f172a;
            margin-bottom: 20px;
        }

        .about-intro p {
            color: #64748b;
            font-size: 16px;
            line-height: 1.9;
            margin-bottom: 15px;
        }

        .about-highlight {
            background: white;
            border-radius: 18px;
            padding: 40px;
            box-shadow: 0 10px 30px rgba(15, 23, 42, 0.08);
            border-left: 5px solid #0f766e;
        }

        .about-highlight h3 {
            margin-top: 0;
            color: #0f766e;
            font-size: 24px;
        }

        .about-highlight p {
            color: #64748b;
            line-height: 1.8;
        }

        /* ==========================================
           CARDS
           ========================================== */

        .section-title {
            text-align: center;
            margin-bottom: 40px;
        }

        .section-title h2 {
            font-size: 34px;
            color: #0f172a;
            margin-bottom: 10px;
        }

        .section-title p {
            color: #64748b;
            font-size: 16px;
        }

        .about-cards {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 25px;
            margin-bottom: 70px;
        }

        .about-card {
            background: white;
            padding: 35px 30px;
            border-radius: 16px;
            text-align: center;
            box-shadow: 0 8px 25px rgba(15, 23, 42, 0.07);
            transition: all 0.3s ease;
        }

        .about-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 15px 35px rgba(15, 23, 42, 0.12);
        }

        .card-icon {
            width: 65px;
            height: 65px;
            margin: 0 auto 20px;
            border-radius: 50%;
            background: #ecfdf5;
            color: #0f766e;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 28px;
            font-weight: bold;
        }

        .about-card h3 {
            color: #0f172a;
            font-size: 22px;
            margin-bottom: 15px;
        }

        .about-card p {
            color: #64748b;
            line-height: 1.8;
            margin: 0;
        }

        /* ==========================================
           VALUES
           ========================================== */

        .values-section {
            background: white;
            border-radius: 20px;
            padding: 50px;
            margin-bottom: 70px;
            box-shadow: 0 8px 25px rgba(15, 23, 42, 0.07);
        }

        .values-grid {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 25px;
        }

        .value-item {
            text-align: center;
            padding: 20px;
        }

        .value-item h4 {
            color: #0f766e;
            font-size: 19px;
            margin-bottom: 10px;
        }

        .value-item p {
            color: #64748b;
            line-height: 1.7;
            font-size: 14px;
        }

        /* ==========================================
           CTA
           ========================================== */

        .about-cta {
            background: linear-gradient(
                135deg,
                #0f766e,
                #115e59
            );

            border-radius: 20px;
            padding: 60px 30px;
            text-align: center;
            color: white;
        }

        .about-cta h2 {
            font-size: 32px;
            margin: 0 0 15px;
        }

        .about-cta p {
            max-width: 700px;
            margin: 0 auto 25px;
            line-height: 1.8;
            color: #d1fae5;
        }

        .contact-button {
            display: inline-block;
            background: white;
            color: #0f766e;
            padding: 13px 28px;
            border-radius: 8px;
            text-decoration: none;
            font-weight: 700;
            transition: all 0.3s ease;
        }

        .contact-button:hover {
            background: #f1f5f9;
        }

        /* ==========================================
           RESPONSIVE
           ========================================== */

        @media (max-width: 900px) {

            .about-intro {
                grid-template-columns: 1fr;
            }

            .about-cards {
                grid-template-columns: 1fr;
            }

            .values-grid {
                grid-template-columns: repeat(2, 1fr);
            }

        }

        @media (max-width: 600px) {

            .about-hero {
                padding: 60px 20px;
            }

            .about-hero h1 {
                font-size: 34px;
            }

            .about-hero p {
                font-size: 15px;
            }

            .about-container {
                padding: 45px 18px;
            }

            .about-intro h2 {
                font-size: 28px;
            }

            .values-section {
                padding: 30px 20px;
            }

            .values-grid {
                grid-template-columns: 1fr;
            }

            .about-cta {
                padding: 45px 20px;
            }

        }

    </style>


    <!-- ==========================================
         ABOUT PAGE
         ========================================== -->

    <div class="about-page">


        <!-- ======================================
             HERO
             ====================================== -->

        <section class="about-hero">

            <h1>About Us</h1>

            <p>
                Building a trusted, transparent and technology-driven
                investment experience for investors in Bangladesh.
            </p>

        </section>


        <!-- ======================================
             MAIN CONTENT
             ====================================== -->

        <div class="about-container">


            <!-- ==================================
                 INTRO
                 ================================== -->

            <section class="about-intro">

                <div>

                    <h2>
                        Empowering Investors Through Better Service
                    </h2>

                    <p>
                        We are committed to providing professional,
                        transparent and efficient services to investors
                        participating in the Bangladesh capital market.
                    </p>

                    <p>
                        Our investor management system helps organize
                        investor information and provides authorized
                        employees with an efficient digital platform
                        for managing investor-related activities.
                    </p>

                    <p>
                        We believe technology, transparency and
                        professional service can create a better
                        investment experience.
                    </p>

                </div>


                <div class="about-highlight">

                    <h3>
                        Our Commitment
                    </h3>

                    <p>
                        We are dedicated to maintaining professional
                        standards, protecting investor information and
                        continuously improving our services through
                        technology.
                    </p>

                    <p>
                        Our goal is to build long-term trust with
                        investors through reliable and responsible
                        service.
                    </p>

                </div>

            </section>


            <!-- ==================================
                 MISSION / VISION / PURPOSE
                 ================================== -->

            <section>

                <div class="section-title">

                    <h2>
                        Who We Are
                    </h2>

                    <p>
                        Our core principles guide everything we do.
                    </p>

                </div>


                <div class="about-cards">


                    <!-- Mission -->

                    <div class="about-card">

                        <div class="card-icon">
                            M
                        </div>

                        <h3>
                            Our Mission
                        </h3>

                        <p>
                            To provide reliable, transparent and
                            professional investment-related services
                            while delivering an efficient experience
                            to investors.
                        </p>

                    </div>


                    <!-- Vision -->

                    <div class="about-card">

                        <div class="card-icon">
                            V
                        </div>

                        <h3>
                            Our Vision
                        </h3>

                        <p>
                            To become a trusted and technology-driven
                            investment service platform that supports
                            investors with modern digital solutions.
                        </p>

                    </div>


                    <!-- Goal -->

                    <div class="about-card">

                        <div class="card-icon">
                            G
                        </div>

                        <h3>
                            Our Goal
                        </h3>

                        <p>
                            To continuously improve investor services,
                            operational efficiency and information
                            management through technology.
                        </p>

                    </div>


                </div>

            </section>


            <!-- ==================================
                 CORE VALUES
                 ================================== -->

            <section class="values-section">

                <div class="section-title">

                    <h2>
                        Our Core Values
                    </h2>

                    <p>
                        The principles that define our service.
                    </p>

                </div>


                <div class="values-grid">


                    <div class="value-item">

                        <h4>
                            Transparency
                        </h4>

                        <p>
                            We believe in clear and responsible
                            communication.
                        </p>

                    </div>


                    <div class="value-item">

                        <h4>
                            Integrity
                        </h4>

                        <p>
                            We maintain professional and ethical
                            standards in our work.
                        </p>

                    </div>


                    <div class="value-item">

                        <h4>
                            Security
                        </h4>

                        <p>
                            We value the security and confidentiality
                            of investor information.
                        </p>

                    </div>


                    <div class="value-item">

                        <h4>
                            Innovation
                        </h4>

                        <p>
                            We use technology to improve services
                            and operational efficiency.
                        </p>

                    </div>


                </div>

            </section>


            <!-- ==================================
                 CTA
                 ================================== -->

            <section class="about-cta">

                <h2>
                    Have Questions About Our Services?
                </h2>

                <p>
                    Our team is ready to assist you with your
                    investment-related questions and service needs.
                </p>

                <a href="Contact.aspx" class="contact-button">
                    Contact Us
                </a>

            </section>


        </div>

    </div>

</asp:Content>