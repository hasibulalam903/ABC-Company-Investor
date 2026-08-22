<%@ Page Title="About Us"
    Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="About.aspx.vb"
    Inherits="About" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style type="text/css">
        .about-page {
            padding: 50px 20px;
            background: #f5f7fa;
            min-height: 80vh;
        }

        .about-content {
            max-width: 1100px;
            margin: 0 auto;
        }

        .about-hero {
            text-align: center;
            background: #0f766e;
            color: white;
            padding: 60px 20px;
            border-radius: 15px;
            margin-bottom: 40px;
        }

        .about-hero h1 {
            font-size: 42px;
            margin: 0 0 15px 0;
        }

        .about-hero p {
            max-width: 750px;
            margin: 0 auto;
            font-size: 17px;
            line-height: 1.7;
        }

        .about-grid {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 25px;
        }

        .about-card {
            background: white;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 5px 20px rgba(0, 0, 0, 0.08);
        }

        .about-card h2 {
            color: #0f766e;
            margin-top: 0;
            margin-bottom: 15px;
        }

        .about-card p {
            color: #64748b;
            line-height: 1.8;
            margin-bottom: 0;
        }

        @media (max-width: 768px) {
            .about-grid {
                grid-template-columns: 1fr;
            }

            .about-hero h1 {
                font-size: 32px;
            }
        }
    </style>

    <div class="about-page">

        <div class="about-content">

            <div class="about-hero">
                <h1>About Us</h1>

                <p>
                    We are committed to providing reliable,
                    transparent and professional investment-related
                    services to our investors.
                </p>
            </div>

            <div class="about-grid">

                <div class="about-card">
                    <h2>Who We Are</h2>

                    <p>
                        ABC Company is a professional investment and
                        investor management organization focused on
                        providing efficient and reliable services.
                    </p>
                </div>

                <div class="about-card">
                    <h2>Our Mission</h2>

                    <p>
                        Our mission is to provide transparent,
                        efficient and technology-driven services
                        that help create a better investor experience.
                    </p>
                </div>

                <div class="about-card">
                    <h2>Our Vision</h2>

                    <p>
                        Our vision is to become a trusted,
                        technology-driven investment service platform
                        in Bangladesh.
                    </p>
                </div>

            </div>

        </div>

    </div>

</asp:Content>