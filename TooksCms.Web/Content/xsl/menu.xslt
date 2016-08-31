<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes" encoding="utf-8" omit-xml-declaration="yes"/>
  <xsl:param name="isFlyout"></xsl:param>
  <xsl:param name="userInfo"></xsl:param>
  <xsl:template match="/">
    <xsl:for-each select="menu">
      <xsl:element name="ul">
        <xsl:attribute name="class">
          <xsl:text>main-menu</xsl:text>
          <xsl:if test="$isFlyout = 'true'">
            <xsl:text> flyout</xsl:text>
          </xsl:if>
        </xsl:attribute>
        <li class="menu">
          <a class="fa fa-reorder"></a>
        </li>
        <li class="logo">
          <a class="brand" href="/">
            <span>Dig-Ec</span>
          </a>
        </li>
        <li class="sign-in submenu collapsable pull-right no-after">
          <a></a>
          <xsl:if test="$isFlyout != 'true'">
            <xsl:if test="$userInfo != ''">
              <div class="dropdown-content">
                <div class="header-popout" id="user-info-host">
                  <p>
                    <xsl:value-of select="$userInfo"/>
                  </p>
                  <input type="button" class="sign-out" onclick="signOut(false);return false;" value="Sign Out" />
                </div>
              </div>
            </xsl:if>
          </xsl:if>
        </li>
        <li class="follow submenu pull-right no-after">
          <a class="follow-trigger click-parent">
            <span class="fb"></span>
            <span class="tw"></span>
            <span class="gp"></span>
            <span class="gh"></span>
          </a>
          <div class="click-content">
            <div class="header-popout social-media" target="_blank" >
              <a href="https://www.facebook.com/timcrittenden">
                <i class="fa fa-facebook"></i>
              </a>
              <a href="https://twitter.com/tooksnet" target="_blank">
                <i class="fa fa-twitter"></i>
              </a>
              <a href="https://plus.google.com/+TimothyCrittenden" target="_blank">
                <i class="fa fa-google-plus"></i>
              </a>
              <a href="https://github.com/t00ks" target="_blank">
                <i class="fa fa-github"></i>
              </a>
            </div>
          </div>
        </li>
        <li class="nav-search submenu pull-right no-after no-click">
          <a class="search-trigger click-parent"></a>
          <div class="click-content">
            <div class="header-popout site-search">
              <input type="text" class="form-control tb-search" placeholder="Search Site..." />
              <button class="btn btn-default btn-primary btn-search">
                Go
              </button>
            </div>
          </div>
        </li>
        <xsl:for-each select="menu-tab">
          <xsl:variable name="number" select="position()" />
          <xsl:variable name="link-type" select="./@link-type" />
          <xsl:variable name="link" select="./@link" />
          <xsl:variable name="name" select="./@name"/>
          <xsl:element name="li">

            <xsl:attribute name="id">
              <xsl:text>menu-item-</xsl:text>
              <xsl:value-of select="$number"/>
            </xsl:attribute>

            <xsl:attribute name="class">
              <xsl:text>collapsable sitenav</xsl:text>
              <xsl:if test="menu-item" >
                <xsl:text> submenu</xsl:text>
              </xsl:if>
            </xsl:attribute>

            <xsl:element name="a">
              <xsl:if test="$link-type != 'none'">
                <xsl:attribute name="href">
                  <xsl:value-of select="@link"/>
                </xsl:attribute>
              </xsl:if>
              <xsl:value-of select="$name"/>
            </xsl:element>

            <xsl:if test="menu-item" >
              <div class="submenu-content">
                <div>
                  <ul>
                    <xsl:for-each select="menu-item">
                      <xsl:variable name="sub-link-type" select="./@link-type" />
                      <xsl:variable name="sub-link" select="./@link" />
                      <xsl:variable name="sub-name" select="./@name"/>
                      <xsl:element name="li">
                        <xsl:element name="a">
                          <xsl:if test="$sub-link-type != 'none'">
                            <xsl:attribute name="href">
                              <xsl:value-of select="$sub-link"/>
                            </xsl:attribute>
                          </xsl:if>
                          <xsl:value-of select="$sub-name"/>
                        </xsl:element>
                      </xsl:element>
                    </xsl:for-each>
                  </ul>
                </div>
              </div>
            </xsl:if>

          </xsl:element>
        </xsl:for-each>
      </xsl:element>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
