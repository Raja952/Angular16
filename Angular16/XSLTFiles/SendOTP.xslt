<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
	<xsl:param name="UserName"></xsl:param>
	<xsl:param name="OTP"></xsl:param>
	
	<!--<xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
		
		
    </xsl:template>-->
	<xsl:template match="/">
		<html>
			<head>

				<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>

				<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
				<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
				<meta name="description" content=""/>
				<meta name="author" content=""/>
			</head>

			<body>
				
				<table>
					<tr>
						<td>
							<p style="margin:0px; font-size:16px; line-height:30px;">
								Hi <b>
									<xsl:value-of select="string($UserName)" />
								</b>,<br /><br />
								Thank you for choosing Your Brand. Use the following OTP to complete your Sign Up procedures. OTP is valid for 5 minutes <a>
									
								</a> is
								<span style="color:GoldenRod;font-size: 20px;">
									<center style="color:GoldenRod;font-size: 20px;">
										<b>
										<xsl:value-of select="string($OTP)" />
										</b>
									</center>
									
								</span>

								<br/><br/>
								Regards,<br/>
								<!--<span style="color:#881735;">-->
								<span style="color:GoldenRod">
									<b>
										Vikas Furniture
									</b>
								</span>
							</p>
						</td>
					</tr>
				</table>
				
				<!--<table cellpadding="0" cellspacing="0" border="0" style="margin:50px auto;width:700px;">
					<tr>
						<td style=" box-shadow: 0px 0px 10px rgba(153,153,153,0.5);padding:10px 20px 20px;font-family:roboto, sans-serif;border-top: 15px solid GoldenRod;border-radius: 10px;">
							<p style="margin:0px; font-size:16px; line-height:30px;">
								Dear <b>
									<xsl:value-of select="string($UserName)" />
								</b>,<br /><br />
								OTP to login <a>
									
								</a> is
								<span style="color:#000000;font-size: 20px;">
									<b>
										<xsl:value-of select="string($OTP)" />
									</b>
								</span>. Do not share this with anyone.

								<br/><br/>
								Regards,<br/>
								--><!--<span style="color:#881735;">--><!--
								<span style="color:GoldenRod">
									<b>
										Vikas Furniture
									</b>
								</span>
							</p>
						</td>
					</tr>
				</table>-->
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
